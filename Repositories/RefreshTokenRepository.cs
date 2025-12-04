using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repositories
{
    public class RefreshTokenRepository: BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken> GetByToken(string token)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<int?> ValidateRefreshToken(string token, string? ipAddress)
        {
            var rt = await _context.RefreshTokens
                .FirstOrDefaultAsync(t =>
                    t.Token == token &&
                    t.RevokedAt == null &&
                    t.ExpiresAt > DateTime.UtcNow
                );

            if (rt == null)
                return null;

            // ❗ Loại bỏ IP-check để không bị auto logout
            // if (!string.IsNullOrEmpty(ipAddress) && rt.CreatedByIp != ipAddress)
            //     return null;

            return rt.UserId;
        }


    }

}
