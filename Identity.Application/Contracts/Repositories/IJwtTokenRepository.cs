using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Repositories
{
    public interface IJwtTokenRepository
    {
        string GenerateToken(IdentityUser user, IList<string> roles);
    }
}
