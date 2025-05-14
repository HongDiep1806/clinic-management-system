using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;

namespace ClinicManagementSystem.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<int> GetRoleIdByName(string roleName)
        {
            var roles = await _roleRepository.GetAll();
            var role = roles.FirstOrDefault(r => r.RoleName.ToLower().Equals(roleName.ToLower()) );
            return role.RoleId;
        }

        public async Task<string> GetRoleNameById(int roleId)
        {
            return await _roleRepository.GetRoleNameById(roleId);
        }
    }
}
