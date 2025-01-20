using Identity.Application.Contracts;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(ApplicationUser user)
        {
            var result = await _userService.UpdateUserAsync(user);
            if (!result) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("Deactivate/{id}")]
        public async Task<ActionResult> DeactivateUser(string id)
        {
            var result = await _userService.DeactivateUserAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("Activate/{id}")]
        public async Task<ActionResult> ActivateUser(string id)
        {
            var result = await _userService.ActivateUserAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
