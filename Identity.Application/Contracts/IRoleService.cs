using Identity.Application.Contracts.Models;

namespace Identity.Application.Contracts
{
    public interface IRoleService
    {
        Task<bool> CreateRole(string name);
        Task<bool> UpdateRole(RoleDto role);
        Task<RoleDto> GetRole(string id);
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task<bool> DeleteRole(string id);
    }
}
