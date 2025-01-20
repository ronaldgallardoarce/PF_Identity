using Identity.Application.Contracts;
using Identity.Application.Contracts.Models;
using Identity.Application.Contracts.Repositories;
using Identity.Domain.Entities;

namespace Identity.Application.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;

        public UserService(
            IUserRepository userRepository,
            IEmailService emailService,
            IEmailTemplateService emailTemplateService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userRepository.UpdateUserAsync(user);
            if (result)
            {
                var emailToSend = new EmailToSend
                {
                    To = user.Email,
                    Subject = "Your profile has been updated",
                    Body = _emailTemplateService.GenerateUserUpdatedEmail(user.UserName)
                };
                await _emailService.SendEmailAsync(emailToSend);
            }
            return result;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                var result = await _userRepository.DeleteUserAsync(id);
                if (result)
                {
                    var emailToSend = new EmailToSend
                    {
                        To = user.Email,
                        Subject = "Your account has been deleted",
                        Body = _emailTemplateService.GenerateUserDeletedEmail(user.UserName)
                    };
                    await _emailService.SendEmailAsync(emailToSend);
                }
                return result;
            }
            return false;
        }

        public async Task<bool> DeactivateUserAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                var result = await _userRepository.DeactivateUserAsync(id);
                if (result)
                {
                    var emailToSend = new EmailToSend
                    {
                        To = user.Email,
                        Subject = "Your account has been deactivated",
                        Body = _emailTemplateService.GenerateUserDeactivatedEmail(user.UserName)
                    };
                    await _emailService.SendEmailAsync(emailToSend);
                }
                return result;
            }
            return false;
        }

        public async Task<bool> ActivateUserAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                var result = await _userRepository.ActivateUserAsync(id);
                if (result)
                {
                    var emailToSend = new EmailToSend
                    {
                        To = user.Email,
                        Subject = "Tu cuenta fue activada",
                        Body = _emailTemplateService.GenerateUserActivatedEmail(user.UserName)
                    };
                    await _emailService.SendEmailAsync(emailToSend);
                }
                return result;
            }
            return false;
        }

    }
}
