using ClinicManagementSystem.DTOs.Auth.Login;
using ClinicManagementSystem.Features.Auth.Commands;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Auth.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponseDto>
    {
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RefreshTokenCommandHandler(
            IRefreshTokenService refreshTokenService,
            IUserService userService,
            IUserRoleService userRoleService,
            IJwtService jwtService,
            IHttpContextAccessor httpContextAccessor
         )
        {
            _refreshTokenService = refreshTokenService;
            _userService = userService;
            _userRoleService = userRoleService;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            var userId = await _refreshTokenService.ValidateRefreshToken(request.RefreshToken, ipAddress);
            if (userId == null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            var user = await _userService.GetUserById(userId.Value);

            var roles = await _userRoleService.GetUserRoles(userId.Value);

            var newAccessToken = _jwtService.GenerateAccessToken(user, roles);
            var newRefreshToken = _jwtService.GenerateRefreshToken();
            await _refreshTokenService.RevokeToken(request.RefreshToken, ipAddress);
            await _refreshTokenService.SaveRefreshToken(user.UserId, newRefreshToken, ipAddress);
            var vnZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var vnTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddMinutes(5), vnZone);


            return new LoginResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = vnTime.ToString("dd/MM/yyyy HH:mm:ss")
            };
        }
    }
}

