using ClinicManagementSystem.DTOs.User;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository,
            IPasswordHasher<User> passwordHasher, IEmailService emailService)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }
        public async Task<User> CreateUser(User user)
        {
            bool exists = await _userRepository.EmailExists(user.Email);
            if (exists)
                throw new BadHttpRequestException("EMAIL_IN_USE", 409);

            return await _userRepository.Create(user);
        }

        public async Task AssignRoleToUser(int userId, int roleId)
        {
            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };
            await _userRoleRepository.Create(userRole);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");

            }
            else
            {
                return user;
            }
        }

        public async Task<List<User>> GetAllPatients()
        {
            return await _userRepository.GetPatientUsers();
        }

        public async Task<List<User>> GetAllDoctors()
        {
            return await _userRepository.GetDoctorUsers();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetById(userId);
        }

        public Task<bool> DoesDoctorExits(int doctorId)
        {
            return _userRepository.DoesDoctorExits(doctorId);
        }
        public Task<bool> DoesPatientExists(int patientId)
        {
            return _userRepository.DoesPatientExists(patientId);
        }

        public async Task<List<User>> GetUsersByRole(string roleName)
        {
            return await _userRepository.GetUsersByRole(roleName);
        }

        public async Task<User> EditUser(int userId, EditUserDto dto)
        {
            return await _userRepository.EditUser(userId, dto);
        }


        public Task<bool> DeleteUser(int userId)
        {
            return _userRepository.DeleteUser(userId);
        }

        public Task<List<dynamic>> GetAllDoctorsWithStatus()
        {
            return _userRepository.GetAllDoctorsWithStatus();
        }

        public Task<List<dynamic>> GetAllPatientsWithStatus()
        {
            return _userRepository.GetAllPatientsWithStatus();
        }

        public Task<List<UserStatusDto>> GetUsersByRoleWithStatus(string roleName)
        {
            return _userRepository.GetUsersByRoleWithStatus(roleName);
        }

        public Task<bool> ToggleUserStatus(int userId)
        {
            return _userRepository.ToggleUserStatus(userId);
        }
        public async Task<bool> RestoreUserWithNewEmail(int userId, string newEmail)
        {
            var deletedUser = await _userRepository.GetDeletedUserById(userId);
            if (deletedUser == null)
                throw new Exception("USER_NOT_FOUND");

            // Check email conflict
            bool exists = await _userRepository.EmailExists(newEmail);
            if (exists)
                throw new Exception("EMAIL_IN_USE");

            return await _userRepository.RestoreUser(deletedUser, newEmail);
        }
        public async Task<dynamic?> GetUserByIdWithStatus(int userId)
        {
            return await _userRepository.GetUserByIdWithStatus(userId);
        }

        public async Task<List<dynamic>> GetAllReceptionistsWithStatus()
        {
            return await _userRepository.GetAllReceptionistsWithStatus();
        }
        public async Task<bool> ChangePassword(int userId, string currentPassword, string newPassword)
        {
            return await _userRepository.ChangePasswordAsync(userId, currentPassword, newPassword);
        }
        public async Task<User?> GetByResetToken(string token)
        {
            return await _userRepository.GetByResetToken(token);
        }

        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                return;

            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            user.ResetPasswordToken = token;
            user.ResetTokenExpires = DateTime.UtcNow.AddMinutes(15);

            await _userRepository.Update(user.UserId, user);

            var link = $"https://clinic.sys/reset-password?token={token}";
            await _emailService.SendEmailAsync(
     user.Email,
     "Reset Password",
     $"<p>Click the link below to reset your password:</p><a href='{link}'>Reset Password</a>"
 );
        }
        public async Task ResetPasswordAsync(string token, string newPassword)
        {
            var user = await _userRepository.GetByResetToken(token);

            if (user == null)
                throw new BadHttpRequestException("INVALID_OR_EXPIRED_TOKEN", 400);

            var hashed = _passwordHasher.HashPassword(user, newPassword);

            user.HashPassword = hashed;
            user.ResetPasswordToken = null;
            user.ResetTokenExpires = null;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.Update(user.UserId, user);
        }
    }
}
