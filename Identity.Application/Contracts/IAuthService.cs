using Identity.Application.Contracts.Models;
using Identity.Domain.Entities;

namespace Identity.Application.Contracts
{
    public interface IAuthService
    {
        Task<TokenDto> Login(LoginDto loginDto);
        Task<bool> AddUser(ApplicationUser usuario);

    }
}
