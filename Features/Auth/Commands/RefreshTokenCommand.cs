using ClinicManagementSystem.DTOs.Auth.Login;
using MediatR;

namespace ClinicManagementSystem.Features.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<LoginResponseDto>
    {
        public string RefreshToken { get; set; }

        public RefreshTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
