namespace Identity.Application.Contracts
{
    public interface IEmailTemplateService
    {
        string GenerateRegisterUserEmail(string userName, int code, string email);
        string GenerateChangePasswordEmail();
        string GenerateUserUpdatedEmail(string userName);
        string GenerateUserDeletedEmail(string userName);
        string GenerateUserDeactivatedEmail(string userName);
        string GenerateUserActivatedEmail(string userName);
    }
}
