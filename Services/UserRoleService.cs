
using ClinicManagementSystem.Repositories;

namespace ClinicManagementSystem.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        public async Task<List<string>> GetUserRoles(int userId)
        {
           return await _userRoleRepository.GetUserRoles(userId);
        }
    }
}
