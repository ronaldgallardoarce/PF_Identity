using Identity.Domain.Entities;

namespace Identity.Application.Contracts
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(ApplicationUser user);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> DeactivateUserAsync(string id);
        Task<bool> ActivateUserAsync(string id);
    }
}
