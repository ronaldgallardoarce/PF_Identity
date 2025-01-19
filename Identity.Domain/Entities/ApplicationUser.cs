using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string VerificationCode {  get; set; } = string.Empty;   
        public bool IsActive { get; set; }
    }
}
