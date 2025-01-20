namespace Identity.Application.Contracts
{
    public interface IEmailTemplateService
    {
        string GenerateRegisterUserEmail(string userName, int code, string email);
        string GenerateChangePasswordEmail();
    }
}
