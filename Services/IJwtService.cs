using ClinicManagementSystem.Models;
using System.Security.Claims;

namespace ClinicManagementSystem.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user, List<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
