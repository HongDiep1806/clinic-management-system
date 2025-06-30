using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<List<User>> GetPatientUsers();
        Task<List<User>> GetDoctorUsers();  
        Task<bool> DoesDoctorExits(int doctorId);
        Task<bool> DoesPatientExists(int patientId);
        Task<List<User>> GetUsersByRole(string roleName);
    }
}
