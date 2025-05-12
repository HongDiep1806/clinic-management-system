using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IRefreshTokenRepository: IBaseRepository<RefreshToken>
    {
       Task<RefreshToken> GetByToken(string token);
    }
    
}
