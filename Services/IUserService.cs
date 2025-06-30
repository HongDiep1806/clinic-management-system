using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IUserService
    {
        Task<User>CreateUser(User user);
        Task AssignRoleToUser(int userId, int roleId);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetAllPatients();
        Task<List<User>> GetAllDoctors();
        Task<User> GetUserById(int userId);
        Task<bool> DoesDoctorExits(int doctorId);
        Task<bool> DoesPatientExists(int patientId); 
        Task<List<User>> GetUsersByRole(string roleName);
    }
}
