using Identity.Application.Contracts;
using Identity.Application.Contracts.Models;
using Identity.Application.Contracts.Repositories;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserProfileService(IUserProfileRepository userProfileRepository,
                                UserManager<ApplicationUser> userManager)
        {
            _userProfileRepository = userProfileRepository;
            _userManager = userManager;
        }
        public async Task<UserProfileResponse> CreateUserProfile(UserProfileDto userProfile)
        {
            var existingUser = await _userManager.FindByEmailAsync(userProfile.Email);

            if (existingUser != null)
            {
                return new UserProfileResponse
                {
                    Success = false,
                    Message = "El usuario ya existe"
                };
            }

            var newUser = new ApplicationUser
            {
                UserName = userProfile.Email,
                Email = userProfile.Email
            };

            var createUserResult = await _userManager.CreateAsync(newUser, "DefaultPassword123!");

            if (!createUserResult.Succeeded)
            {
                return new UserProfileResponse
                {
                    Success = false,
                    Message = "Error al crear el usuario: " + string.Join(", ", createUserResult.Errors.Select(e => e.Description))
                };
            }

            var profileResult = await _userProfileRepository.CreateUserProfile(newUser.Id, userProfile);

            return new UserProfileResponse
            {
                Success = profileResult,
                Message = profileResult ? "Perfil creado exitosamente" : "Error al crear el perfil",
                Data = profileResult ? userProfile : null
            };
        }

        public async Task<UserProfileResponse> GetUserProfile(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new UserProfileResponse
                {
                    Success = false,
                    Message = "Usuario no encontrado"
                };
            }

            var profile = await _userProfileRepository.GetUserProfile(user.Id);
            if (profile == null)
            {
                return new UserProfileResponse
                {
                    Success = false,
                    Message = "Perfil no encontrado"
                };
            }

            return new UserProfileResponse
            {
                Success = true,
                Message = "Perfil encontrado exitosamente",
                Data = profile
            };
        }

        public async Task<UserProfileResponse> UpdateUserProfile(UserProfileDto userProfile)
        {
            var user = await _userManager.FindByEmailAsync(userProfile.Email);
            if (user == null)
            {
                return new UserProfileResponse
                {
                    Success = false,
                    Message = "Usuario no encontrado"
                };
            }

            var result = await _userProfileRepository.UpdateUserProfile(user.Id, userProfile);

            return new UserProfileResponse
            {
                Success = result,
                Message = result ? "Perfil actualizado exitosamente" : "Error al actualizar el perfil",
                Data = result ? userProfile : null
            };
        }

        public async Task<UserProfileResponse> DeleteUserProfile(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new UserProfileResponse
                {
                    Success = false,
                    Message = "Usuario no encontrado"
                };
            }

            var result = await _userProfileRepository.DeleteUserProfile(user.Id);

            return new UserProfileResponse
            {
                Success = result,
                Message = result ? "Perfil eliminado exitosamente" : "Error al eliminar el perfil"
            };
        }

        public async Task<IEnumerable<UserProfileDto>> GetAllUserProfiles()
        {
            return await _userProfileRepository.GetAllUserProfiles();
        }
    }
}
