namespace ClinicManagementSystem.Services
{
    public interface IRefreshTokenService
    {
        Task RevokeToken(string refreshToken, string? ipAddress);
        Task SaveRefreshToken(int userId, string token, string ipAddress);

    }
}