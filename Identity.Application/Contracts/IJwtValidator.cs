using System.Security.Claims;

namespace Identity.Application.Contracts
{
    public interface IJwtValidator
    {
        ClaimsPrincipal? ValidateToken(string token);
    }
}
