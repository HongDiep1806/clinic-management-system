using MediatR;

namespace ClinicManagementSystem.Features.Auth.Commands
{
    public class LogoutCommand : IRequest
    {
        public string RefreshToken { get; set; }
    }
}
