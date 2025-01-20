using Identity.Application.Contracts.Models;
using Identity.Domain.Entities;

namespace Identity.Application.Contracts.Repositories
{
    public interface IAuthRepository
    {
        Task<TokenDto> Login(LoginDto loginDto, string ipAddress);
        Task<bool> AddUser(ApplicationUser usuario);
        Task<int> AddVerificationCode(ApplicationUser user);
        Task<bool> ChangePassword(ChangePasswordDto changePassword);
        Task<bool> ConfirmEmail(string email, string code);
        Task<bool> LogoutAsync(string userId);
        Task<TokenDto> RefreshTokenAsync(string RefreshToken, string ipAddress);
    }
}
