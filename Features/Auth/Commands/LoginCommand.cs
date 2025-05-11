using ClinicManagementSystem.DTOs.Auth;
using MediatR;

namespace ClinicManagementSystem.Features.Auth.Commands
{
    public class LoginCommand : IRequest<AuthResponseDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
