using Identity.Application.Contracts.Models;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts
{
    public interface IAuthService
    {
        Task<TokenDto> Login(LoginDto loginDto);
        Task<bool> AddUser(ApplicationUser usuario);

    }
}
