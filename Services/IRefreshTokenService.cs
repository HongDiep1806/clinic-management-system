namespace ClinicManagementSystem.Services
{
    public interface IRefreshTokenService
    {
        Task SaveRefreshToken(int userId, string token, string ipAddress);

    }
}
