using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<User?> GetByEmailAsync(string email)
        {

            return await _context.Users
                .Include(u => u.Department)
                .Include(u => u.AppointmentsAsPatient)
                .Include(u => u.AppointmentsAsDoctor)
                .Include(u => u.Schedules)
                .Include(u => u.UserRoles)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
