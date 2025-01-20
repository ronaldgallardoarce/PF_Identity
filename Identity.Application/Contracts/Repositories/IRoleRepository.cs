using Identity.Application.Contracts.Models;
using Identity.Domain.Entities;

namespace Identity.Application.Contracts.Repositories
{
    public interface IRoleRepository
    {
        Task<bool> CreateRole(string name);
        Task<bool> UpdateRole(RoleDto role);
        Task<RoleDto> GetRole(string id);
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task<bool> DeleteRole(string id);
        Task<bool> RoleExistAsync(string name);
        Task<bool> AssignRoleToUserAsync(ApplicationUser user, string roleName);
    }
}
