using ClinicManagementSystem.DTOs.User;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Models.Deleted;

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
        Task<User> EditUser(int userId, EditUserDto dto);
        Task<bool> DeleteUser(int userId);
        Task<List<dynamic>> GetAllPatientsWithStatus();

        Task<List<dynamic>> GetAllDoctorsWithStatus();
        Task<List<dynamic>> GetAllReceptionistsWithStatus();
        Task<List<UserStatusDto>> GetUsersByRoleWithStatus(string roleName);
        Task<bool> ToggleUserStatus(int userId);
        Task<DeletedUser?> GetDeletedUserById(int userId);
        Task<bool> EmailExists(string email);
        Task<bool> RestoreUser(DeletedUser deletedUser, string newEmail);
        Task<dynamic?> GetUserByIdWithStatus(int userId);
        Task<User?> GetByIdWithDepartment(int userId);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);



    }
}
