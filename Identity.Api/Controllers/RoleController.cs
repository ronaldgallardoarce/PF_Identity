using Identity.Application.Contracts;
using Identity.Application.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpPost("CreateRole")]
        public async Task<ActionResult> CreateRole(string name)
        {
            var result = await _roleService.CreateRole(name);
            if (result)
            {
                return Created();
            }
            return BadRequest();
        }
        [HttpDelete("DeleteRole/{id}")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            var result = await _roleService.DeleteRole(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _roleService.GetAllRoles();
            return result != null ? Ok(result) : BadRequest();
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetRole(string id)
        {
            var result = await _roleService.GetRole(id);
            return result != null ? Ok(result) : NotFound();
        }
        [HttpPut("UpdateRole")]
        public async Task<ActionResult> UpdateRole(RoleDto role)
        {
            var result = await _roleService.UpdateRole(role);
            return result ? Ok(result) : BadRequest();
        }
    }
}
