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
        public async Task<string> GetRoleNameById(int roleId)
        {
            return await _roleRepository.GetRoleNameById(roleId);
        }
    }
}
