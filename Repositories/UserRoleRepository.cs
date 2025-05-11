using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public class UserRoleRepository: BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
