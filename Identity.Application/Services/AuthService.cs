using Identity.Application.Contracts;
using Identity.Application.Contracts.Models;
using Identity.Application.Contracts.Repositories;
using Identity.Domain.Entities;

namespace Identity.Application.Services
{
    public class AuthService : IAuthService
    {
        private IAuthRepository _authRepository;
        private IRoleRepository _roleRepository;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        
        public AuthService(IAuthRepository authRepository, IRoleRepository roleRepository,IEmailService emailService, IEmailTemplateService emailTemplateService)
        {
            _authRepository = authRepository;
            _roleRepository = roleRepository;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
        }

        public async Task<bool> AddUser(RegisterUserDto usuario)
        {
            var user = new ApplicationUser
            {
                UserName = usuario.UserName,
                Email = usuario.Email,
                PasswordHash = usuario.Password
            };
            var result = await _authRepository.AddUser(user);
            if (result)
            {
                int codeForSend = await _authRepository.AddVerificationCode(user);
                await _roleRepository.CreateRole("Client");
                await _roleRepository.AssignRoleToUserAsync(user, "Client");
                if(codeForSend != 0)
                {
                    EmailToSend email = new EmailToSend
                    {
                        To = user.Email,
                        From = null,
                        Subject = "You registered in UPDS",
                        Body = _emailTemplateService.GenerateRegisterUserEmail(user.UserName, codeForSend, user.Email)
                    };
                    await _emailService.SendEmailAsync(email);
                }
                return true;
            }
            return false;
        }

        public Task<TokenDto> Login(LoginDto loginDto)
        {
            var result = _authRepository.Login(loginDto);
            return result;
        }

        public async Task<bool> ChangePassword(ChangePasswordDto changePassword)
        {
            var result = await _authRepository.ChangePassword(changePassword);
            if (result)
            {
                EmailToSend email = new EmailToSend
                {
                    To = changePassword.Email,
                    From = null,
                    Subject = "You changed your password",
                    Body = _emailTemplateService.GenerateChangePasswordEmail()
                };
                await _emailService.SendEmailAsync(email);
                return true;
            }
            return false;
        }
    }
}
