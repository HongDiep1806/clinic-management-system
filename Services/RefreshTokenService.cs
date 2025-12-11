using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ClinicManagementSystem.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _repo;

        public RefreshTokenService(IRefreshTokenRepository repo)
        {
            _repo = repo;
        }

        public async Task SaveRefreshToken(int userId, string token, string ipAddress)
        {
            // ⭐ Đảm bảo ipAddress KHÔNG BAO GIỜ NULL
            ipAddress = string.IsNullOrEmpty(ipAddress) ? "0.0.0.0" : ipAddress;

            var rt = new RefreshToken
            {
                UserId = userId,
                Token = token,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedByIp = ipAddress
            };

            await _repo.Create(rt);
        }

        public async Task RevokeToken(string token, string? ipAddress, string? replacedBy = null)
        {
            // ⭐ Đảm bảo ipAddress không null
            ipAddress = string.IsNullOrEmpty(ipAddress) ? "0.0.0.0" : ipAddress;

            var rt = await _repo.GetByToken(token);
            if (rt == null || !rt.IsActive)
                return;

            rt.RevokedAt = DateTime.UtcNow;
            rt.RevokedByIp = ipAddress;
            rt.ReplacedByToken = replacedBy;

            await _repo.Update(rt.Id, rt);
        }

        public async Task<int?> ValidateRefreshToken(string refreshToken, string? ipAddress)
        {
            // ⭐ Đảm bảo ipAddress không null
            ipAddress = string.IsNullOrEmpty(ipAddress) ? "0.0.0.0" : ipAddress;

            return await _repo.ValidateRefreshToken(refreshToken, ipAddress);
        }
    }
}
