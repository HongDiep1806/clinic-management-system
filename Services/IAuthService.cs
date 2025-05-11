using ClinicManagementSystem.DTOs.Auth;

namespace ClinicManagementSystem.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(string email, string password);
    }
}
