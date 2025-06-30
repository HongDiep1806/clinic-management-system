using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repositories
{
    public interface IRefreshTokenRepository: IBaseRepository<RefreshToken>
    {
       Task<RefreshToken> GetByToken(string token);
        Task<int?> ValidateRefreshToken(string refreshToken, string? ipAddress);
       
    }

}
