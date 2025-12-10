namespace ClinicManagementSystem.Services
{
    public interface IRefreshTokenService
    {
        /// <summary>
        /// Xác thực refresh token (kiểm tra active + chưa hết hạn)
        /// </summary>
        Task<int?> ValidateRefreshToken(string refreshToken, string? ipAddress);

        /// <summary>
        /// Lưu refresh token mới
        /// </summary>
        Task SaveRefreshToken(int userId, string token, string ipAddress);

        /// <summary>
        /// Thu hồi refresh token (có thể kèm token mới để liên kết rotation)
        /// </summary>
        Task RevokeToken(string refreshToken, string? ipAddress, string? replacedByToken = null);
    }
}
