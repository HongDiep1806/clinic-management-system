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
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            var userId = await _refreshTokenService.ValidateRefreshToken(request.RefreshToken, ip);
            if (userId == null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            var user = await _userService.GetUserById(userId.Value);
            var roles = await _userRoleService.GetUserRoles(userId.Value);

            // Generate tokens
            var newAccess = _jwtService.GenerateAccessToken(user, roles);
            var newRefresh = _jwtService.GenerateRefreshToken();

            // Rotation an toàn: revoke token cũ và gắn link sang token mới
            await _refreshTokenService.RevokeToken(request.RefreshToken, ip, newRefresh);

            // Lưu refresh token mới
            await _refreshTokenService.SaveRefreshToken(user.UserId, newRefresh, ip);

            return new LoginResponseDto
            {
                AccessToken = newAccess,
                RefreshToken = newRefresh,
                ExpiresAt = DateTime.Now.AddMinutes(5).ToString("dd-MM-yyyy HH:mm:ss")
            };
        }
    }
}

