using Identity.Application.Contracts;
using Identity.Application.Contracts.Models;
using Identity.Application.Contracts.Repositories;

namespace Identity.Application.Services
{
    public class RoleService : IRoleService
    {
        private IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> CreateRole(string name)
        {
            var result = await _roleRepository.CreateRole(name);
            return result;
        }

        public async Task<bool> DeleteRole(string id)
        {
            var result = await _roleRepository.DeleteRole(id);
            return result;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllRoles();
            return roles;
        }

        public async Task<RoleDto> GetRole(string id)
        {
            var role = await _roleRepository.GetRole(id);
            return role;
        }

        public async Task<bool> UpdateRole(RoleDto role)
        {
            var result = await _roleRepository.UpdateRole(role);
            return result;
        }
    }
}
