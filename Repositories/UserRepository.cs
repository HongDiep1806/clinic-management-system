﻿using Azure.Core;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ClinicManagementSystem.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserRepository(ApplicationDbContext context, IPasswordHasher<User> passwordHasher) : base(context)
        {
            _passwordHasher = passwordHasher;
        }

        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, password);
            return Task.FromResult(result == PasswordVerificationResult.Success);
        }

        public Task<bool> DoesDoctorExits(int doctorId)
        {
            var doctor = _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.UserId == doctorId && u.UserRoles.Any(ur => ur.Role.RoleName == "Doctor"));
            if (doctor == null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DoesPatientExists(int patientId)
        {
            var patient = _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.UserId == patientId && u.UserRoles.Any(ur => ur.Role.RoleName == "Patient"));
            if (patient == null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {

            return await _context.Users
                //.Include(u => u.Department)
                //.Include(u => u.AppointmentsAsPatient)
                //.Include(u => u.AppointmentsAsDoctor)
                //.Include(u => u.Schedules)
                .Include(u => u.UserRoles)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetDoctorUsers()
        {
            return await _context.Set<User>()
               .Include(u => u.UserRoles)
                   .ThenInclude(ur => ur.Role)
               .Where(u => u.UserRoles.Any(ur => ur.Role.RoleName == "Doctor"))
               .ToListAsync();
        }

        public async Task<List<User>> GetPatientUsers()
        {
            return await _context.Set<User>()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.UserRoles.Any(ur => ur.Role.RoleName == "Patient"))
                .ToListAsync();
        }

        public async Task<List<User>> GetUsersByRole(string roleName)
        {
            var users = await _context.Set<User>()  
                                       .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                                       .Include(u => u.Department)
                                       .Where(u => u.UserRoles.Any(ur => ur.Role.RoleName.ToLower() == roleName.ToLower()))
                                       .ToListAsync();
            return users;
        }
    }
}
