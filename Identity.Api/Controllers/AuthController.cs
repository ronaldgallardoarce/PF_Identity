using Identity.Application.Contracts;
using Identity.Application.Contracts.Models;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var result = await _authService.Login(loginDto);
            if(result == null)
            {
                return Unauthorized("Credenciales incorrectas");
            }
            return Ok(result);
        }
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser(RegisterUserDto usuario)
        {
            var result = await _authService.AddUser(usuario);
            if (result) { return Created(); } else { return BadRequest(); }
        }
        [HttpPut("ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePassword)
        {
            var result = await _authService.ChangePassword(changePassword);
            if (result) { return Ok(result); } else { return BadRequest(); }
        }
    }
}
