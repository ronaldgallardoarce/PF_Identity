using Identity.Application.Contracts.Models;
using Identity.Application.Contracts.Repositories;
using Identity.Domain.Entities;
using Identity.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repository
{
    public class UserProfileRepository: IUserProfileRepository
    {
        private readonly ContextPostgreSQL _context;

        public UserProfileRepository(ContextPostgreSQL context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserProfile(string userId, UserProfileDto userProfile)
        {
            var newProfile = new UserProfile
            {
                UserId = userId,
                Ci = userProfile.Ci,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                DateOfBirth = userProfile.DateOfBirth,
                Address = userProfile.Address
            };

            try
            {
                await _context.UserProfiles.AddAsync(newProfile);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserProfileDto> GetUserProfile(string userId)
        {
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (userProfile == null)
                return null;

            return MapToDto(userProfile);
        }

        public async Task<bool> UpdateUserProfile(string userId, UserProfileDto userProfile)
        {
            var existingProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (existingProfile == null)
                return false;

            existingProfile.Ci = userProfile.Ci;
            existingProfile.FirstName = userProfile.FirstName;
            existingProfile.LastName = userProfile.LastName;
            existingProfile.DateOfBirth = userProfile.DateOfBirth;
            existingProfile.Address = userProfile.Address;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserProfile(string userId)
        {
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (userProfile == null)
                return false;

            try
            {
                _context.UserProfiles.Remove(userProfile);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<UserProfileDto>> GetAllUserProfiles()
        {
            var userProfiles = await _context.UserProfiles
                .Include(x => x.User)
                .ToListAsync();

            return userProfiles.Select(MapToDto);
        }

        private UserProfileDto MapToDto(UserProfile userProfile)
        {
            return new UserProfileDto
            {
                Email = userProfile.User?.Email ?? string.Empty,
                Ci = userProfile.Ci,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                DateOfBirth = userProfile.DateOfBirth,
                Address = userProfile.Address
            };
        }
    }
}
