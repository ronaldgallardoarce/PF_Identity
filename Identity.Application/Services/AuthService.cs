using Identity.Application.Contracts;
using Identity.Application.Contracts.Models;
using Identity.Application.Contracts.Repositories;
using Identity.Domain.Entities;

namespace Identity.Application.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddUser(ApplicationUser usuario)
        {
            var result = await _userRepository.AddUser(usuario);
            if (result)
            {
                return true;
            }
            return false;
        }

        public Task<TokenDto> Login(LoginDto loginDto)
        {
            var result = _userRepository.Login(loginDto);
            return result;
        }
    }
}
