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
                .FirstOrDefaultAsync(rt => rt.Token.ToLower().Equals( token));
        }

        public async Task<int?> ValidateRefreshToken(string refreshToken, string? ipAddress)
        {
            var token = await _context.RefreshTokens
               .FirstOrDefaultAsync(t =>
                t.Token == refreshToken &&
                t.RevokedAt == null &&
                t.ExpiresAt > DateTime.UtcNow);

            if (token == null)
                return null;
            if (!string.IsNullOrEmpty(ipAddress) && token.CreatedByIp != ipAddress)
                return null;

            return token.UserId;
        }

    }

}
