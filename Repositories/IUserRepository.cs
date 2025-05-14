using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<List<User>> GetPatientUsers();
        Task<List<User>> GetDoctorUsers();  

    }
}
