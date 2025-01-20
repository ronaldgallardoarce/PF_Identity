using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Models
{
    public class TokenDto
    {
        public string AccessToken {  get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
