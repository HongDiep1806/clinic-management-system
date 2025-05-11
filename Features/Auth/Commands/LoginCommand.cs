using ClinicManagementSystem.DTOs.Auth;
using ClinicManagementSystem.DTOs.Auth.Login;
using MediatR;

namespace ClinicManagementSystem.Features.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
