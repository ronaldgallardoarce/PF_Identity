using Identity.Application.Contracts.Models;

namespace Identity.Application.Contracts.Repositories
{
    public interface IUserProfileRepository
    {
        Task<bool> CreateUserProfile(string userId, UserProfileDto userProfile);
        Task<UserProfileDto> GetUserProfile(string userId);
        Task<bool> UpdateUserProfile(string userId, UserProfileDto userProfile);
        Task<bool> DeleteUserProfile(string userId);
        Task<IEnumerable<UserProfileDto>> GetAllUserProfiles();
    }
}
