using Identity.Application.Contracts.Models;
using Identity.Application.Contracts.Repositories;
using Identity.Domain.Entities;
using Identity.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserManager<ApplicationUser> _context;
        private ContextPostgreSQL _contextPostgres;
        private readonly IJwtTokenRepository _jwtTokenRepository;
        public UserRepository(ContextPostgreSQL contextPostgres, UserManager<ApplicationUser> context, IJwtTokenRepository jwtTokenRepository)
        {
            _contextPostgres = contextPostgres;
            _context = context;
            _jwtTokenRepository = jwtTokenRepository;
        }

        public async Task<TokenDto> Login(LoginDto loginDto)
        {
            var user = await _context.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return null;
            }
            var isPasswordValid = await _context.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return null;

            }
            var roles = await _context.GetRolesAsync(user);
            var token = _jwtTokenRepository.GenerateToken(user, roles);

            return new TokenDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddDays(1)
            };
        }

        public async Task<bool> AddUser(ApplicationUser usuario)
        {
            IdentityResult result = await _context.CreateAsync(usuario, usuario.PasswordHash!);
            if (result.Succeeded)
            {
                return true;
            }
            else { return false; }
        }
    }
}
