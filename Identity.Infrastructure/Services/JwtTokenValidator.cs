using Identity.Application.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Infrastructure.Services
{
    public class JwtTokenValidator: IJwtValidator
    {
        private readonly string _key;

        public JwtTokenValidator(string key)
        {
            _key = key;
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                if (jwtToken == null)
                {
                    return null;
                }

                var iatClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Iat);
                if (iatClaim != null)
                {
                    var issuedAt = DateTimeOffset.FromUnixTimeSeconds(long.Parse(iatClaim.Value));
                    if (issuedAt > DateTimeOffset.UtcNow)
                    {
                        return null;
                    }
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
