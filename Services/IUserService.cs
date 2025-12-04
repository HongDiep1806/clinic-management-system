using ClinicManagementSystem.DTOs.User;
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
        Task<User> EditUser(int userId, EditUserDto dto);
        Task<bool> DeleteUser(int userId);
        Task<List<dynamic>> GetAllDoctorsWithStatus();
        Task<List<dynamic>> GetAllPatientsWithStatus();
        Task<List<UserStatusDto>> GetUsersByRoleWithStatus(string roleName);
        Task<bool> ToggleUserStatus(int userId);
        Task<bool> RestoreUserWithNewEmail(int userId, string newEmail);
        Task<dynamic?> GetUserByIdWithStatus(int userId);



    }
}
