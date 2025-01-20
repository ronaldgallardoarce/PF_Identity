using Identity.Application.Contracts.Models;
using Identity.Domain.Entities;

namespace Identity.Application.Contracts.Repositories
{
    public interface IAuthRepository
    {
        Task<TokenDto> Login(LoginDto loginDto);
        Task<bool> AddUser(ApplicationUser usuario);
        Task<int> AddVerificationCode(ApplicationUser user);
        Task<bool> ChangePassword(ChangePasswordDto changePassword);
    }
}
