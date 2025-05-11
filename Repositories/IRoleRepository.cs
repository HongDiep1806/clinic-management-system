using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IRoleRepository: IBaseRepository<Role>
    {
        Task<string> GetRoleNameById(int roleId);
    }

}
