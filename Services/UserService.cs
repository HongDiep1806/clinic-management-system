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
        public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }
        public async Task<User> CreateUser(User user)
        {
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
    }
}
