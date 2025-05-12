using ClinicManagementSystem.Features.Auth.Commands;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Auth.Handlers
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutCommandHandler(
            IRefreshTokenService refreshTokenService,
            IHttpContextAccessor httpContextAccessor)
        {
            _refreshTokenService = refreshTokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            await _refreshTokenService.RevokeToken(request.RefreshToken, ipAddress);
            return Unit.Value;
        }
    }
}
