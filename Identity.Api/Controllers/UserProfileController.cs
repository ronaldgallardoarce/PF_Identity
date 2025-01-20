using Identity.Application.Contracts.Models;
using Identity.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }


        [HttpPost("Create/{email}")]
        public async Task<ActionResult> CreateUserProfile([FromBody] UserProfileDto userProfile)
        {
            var result = await _userProfileService.CreateUserProfile(userProfile);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetByEmail/{email}")]
        public async Task<ActionResult> GetUserProfile(string email)
        {
            var result = await _userProfileService.GetUserProfile(email);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> UpdateUserProfile(UserProfileDto userProfile)
        {
            var result = await _userProfileService.UpdateUserProfile(userProfile);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("Delete/{email}")]
        public async Task<ActionResult> DeleteUserProfile(string email)
        {
            var result = await _userProfileService.DeleteUserProfile(email);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllUserProfiles()
        {
            var result = await _userProfileService.GetAllUserProfiles();
            return Ok(new { Success = true, Message = "Perfiles recuperados exitosamente", Data = result });
        }
    }
}
