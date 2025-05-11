using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IUserService
    {
        Task<User>CreateUser(User user);
        Task AssignRoleToUser(int userId, int roleId);
        Task<User> GetUserByEmail(string email);
    }
}
