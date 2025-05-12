
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace ClinicManagementSystem.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task RevokeToken(string token, string? ipAddress)
        {
            var refreshToken = await _refreshTokenRepository.GetByToken(token);
            if (refreshToken == null || !refreshToken.IsActive)
                throw new SecurityTokenException("Invalid or expired refresh token.");

            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            await _refreshTokenRepository.Update(refreshToken.Id ,refreshToken);
        }

        public async Task SaveRefreshToken(int userId, string token, string ipAddress)
        {
            await _refreshTokenRepository.Create(new RefreshToken
            {
                UserId = userId,
                Token = token,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                RevokedAt = null,
                CreatedByIp = ipAddress
            });
        }
    }
}
