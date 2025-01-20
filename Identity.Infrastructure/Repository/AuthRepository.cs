using Identity.Application.Contracts.Models;
using Identity.Application.Contracts.Repositories;
using Identity.Domain.Entities;
using Identity.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private UserManager<ApplicationUser> _userManager;
        private ContextPostgreSQL _contextPostgres;
        private readonly IJwtTokenRepository _jwtTokenRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AuthRepository(ContextPostgreSQL contextPostgres, UserManager<ApplicationUser> userManager, IJwtTokenRepository jwtTokenRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _contextPostgres = contextPostgres;
            _userManager = userManager;
            _jwtTokenRepository = jwtTokenRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<TokenDto> Login(LoginDto loginDto, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var accesstoken = _jwtTokenRepository.GenerateToken(user, roles);
            var refreshToken = await _refreshTokenRepository.CreateAsync(user.Id, ipAddress);

            return new TokenDto
            {
                AccessToken = accesstoken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<bool> AddUser(ApplicationUser usuario)
        {
            IdentityResult result = await _userManager.CreateAsync(usuario, usuario.PasswordHash!);
            if (result.Succeeded)
            {
                var userProfile = new UserProfile
                {
                    UserId = usuario.Id,
                };
                await _contextPostgres.UserProfiles.AddAsync(userProfile);
                await _contextPostgres.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task<int> AddVerificationCode(ApplicationUser user)
        {
            Random rnd = new Random();
            int code = rnd.Next(10000, 99999);
            ApplicationUser applicationUser = await _contextPostgres.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if (applicationUser != null)
            {
                applicationUser!.VerificationCode = code.ToString();
                await _contextPostgres.SaveChangesAsync();
                return code;
            }
            return 0;
        }

        public async Task<bool> ChangePassword(ChangePasswordDto changePassword)
        {
            var user = await _userManager.FindByEmailAsync(changePassword.Email);
            if (user != null)
            {
                bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, changePassword.OldPassword);
                if (!isPasswordCorrect)
                {
                    return false;
                }
                IdentityResult result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
                return true;
            }
            return false;
        }

        public async Task<bool> ConfirmEmail(string email, string code)
        {
            var user = await _contextPostgres.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null && user.VerificationCode == code)
            {
                user.EmailConfirmed = true;
                user.IsActive = true;
                await _contextPostgres.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> LogoutAsync(string userId)
        {
            var result = await _refreshTokenRepository.RevokeAllTokensByUserIdAsync(userId);
            return result;
        }

        public async Task<TokenDto> RefreshTokenAsync(string RefreshToken, string ipAddress)
        {
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(RefreshToken);
            if (storedToken == null || storedToken.IsRevoked || storedToken.IsUsed || storedToken.ExpiryDate < DateTime.UtcNow)
                throw new ApplicationException("Refresh token inválido.");

            storedToken.IsUsed = true;
            await _refreshTokenRepository.UpdateAsync(storedToken);

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            if (user == null)
                throw new ApplicationException("Usuario no encontrado.");

            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _jwtTokenRepository.GenerateToken(user, roles);
            var newRefreshToken = await _refreshTokenRepository.CreateAsync(user.Id, ipAddress);

            return new TokenDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };
        }
    }
}
