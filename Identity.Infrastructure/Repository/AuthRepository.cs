using Identity.Application.Contracts.Models;
using Identity.Application.Contracts.Repositories;
using Identity.Domain.Entities;
using Identity.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repository
{
    public class AuthRepository:IAuthRepository
    {
        private UserManager<ApplicationUser> _userManager;
        private ContextPostgreSQL _contextPostgres;
        private readonly IJwtTokenRepository _jwtTokenRepository;
        public AuthRepository(ContextPostgreSQL contextPostgres, UserManager<ApplicationUser> userManager, IJwtTokenRepository jwtTokenRepository)
        {
            _contextPostgres = contextPostgres;
            _userManager = userManager;
            _jwtTokenRepository = jwtTokenRepository;
        }

        public async Task<TokenDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return null;
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return null;

            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenRepository.GenerateToken(user, roles);

            return new TokenDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddDays(1)
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
            ApplicationUser applicationUser = await _contextPostgres.Users.FirstOrDefaultAsync(u=> u.UserName == user.UserName);
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
    }
}
