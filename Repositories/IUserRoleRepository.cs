using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IUserRoleRepository:IBaseRepository<UserRole>
    {
        Task<List<string>> GetUserRoles(int userId);

        
    }
}
