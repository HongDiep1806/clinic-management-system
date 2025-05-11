using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<string> GetRoleNameById(int roleId)
        {
            var role = await GetById(roleId);
            return role?.RoleName ?? string.Empty;
        }
    }

}
