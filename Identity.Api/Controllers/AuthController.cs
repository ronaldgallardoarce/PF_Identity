using Identity.Application.Contracts;
using Identity.Application.Contracts.Models;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await _authService.Login(loginDto, ipAddress);
            if(result == null)
            {
                return Unauthorized("Credenciales incorrectas");
            }
            return Ok(result );
        }
        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateUser(RegisterUserDto usuario)
        {
            var result = await _authService.AddUser(usuario);
            if (result) { return Created(); } else { return BadRequest(); }
        }
        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePassword)
        {
            var result = await _authService.ChangePassword(changePassword);
            if (result) { return Ok(result); } else { return BadRequest(); }
        }
        [HttpPut("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string code)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(code))
            {
                return BadRequest("Email y código son requeridos.");
            }
            var result = await _authService.ConfirmEmail(email, code);
            if (result) { return Ok("El email ha sido confirmado exitosamente."); } else { return BadRequest(); };
        }
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await _authService.RefreshTokenAsync(refreshToken, ipAddress);

            if (result == null)
                return Unauthorized(new { Message = "Token inválido o expirado" });

            return Ok(result);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized(new { Message = "Usuario no autenticado" });

            var result = await _authService.LogoutAsync(userId);
            return NoContent();
        }
    }
}
