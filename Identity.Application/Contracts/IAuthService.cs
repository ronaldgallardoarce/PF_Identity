using Identity.Application.Contracts.Models;

namespace Identity.Application.Contracts
{
    public interface IAuthService
    {
        Task<TokenDto> Login(LoginDto loginDto, string ipAddress);
        Task<bool> AddUser(RegisterUserDto usuario);
        Task<bool> ChangePassword(ChangePasswordDto changePassword);
        Task<bool> ConfirmEmail(string email, string code);
        Task<bool> LogoutAsync(string userId);
        Task<TokenDto> RefreshTokenAsync(string RefreshToken, string ipAddress);
    }
}
