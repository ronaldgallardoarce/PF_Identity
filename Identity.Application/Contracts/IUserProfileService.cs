using Identity.Application.Contracts.Models;

namespace Identity.Application.Contracts
{
    public interface IUserProfileService
    {
        Task<UserProfileResponse> CreateUserProfile(UserProfileDto userProfile);
        Task<UserProfileResponse> GetUserProfile(string email);
        Task<UserProfileResponse> UpdateUserProfile(UserProfileDto userProfile);
        Task<UserProfileResponse> DeleteUserProfile(string email);
        Task<IEnumerable<UserProfileDto>> GetAllUserProfiles();
    }
}
