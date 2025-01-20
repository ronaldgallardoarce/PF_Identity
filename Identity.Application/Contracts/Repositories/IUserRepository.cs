using Identity.Domain.Entities;

namespace Identity.Application.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(ApplicationUser user);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> DeactivateUserAsync(string id);
        Task<bool> ActivateUserAsync(string id);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
    }
}
