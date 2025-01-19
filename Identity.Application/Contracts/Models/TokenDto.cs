using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Models
{
    public class TokenDto
    {
        public string Token {  get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
