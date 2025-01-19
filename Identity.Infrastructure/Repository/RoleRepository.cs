using Identity.Application.Contracts.Models;
using Identity.Application.Contracts.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRole(string name)
        {
            var identityRole = new IdentityRole { Name = name };
            IdentityResult result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return false;
            }
            await _roleManager.DeleteAsync(role);
            return true;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoles()
        {
            var result = await _roleManager.Roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();
            return result;
        }

        public async Task<RoleDto> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if(role == null)
            {
                return null;
            }
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public async Task<bool> UpdateRole(RoleDto role)
        {
            var existingRole = await _roleManager.FindByIdAsync(role.Id);
            if (existingRole != null)
            {
                existingRole.Name = role.Name;
                await _roleManager.UpdateAsync(existingRole);
                return true;
            }
            return false;
        }
    }
}
