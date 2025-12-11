using ClinicManagementSystem.DTOs.Auth.Login;
using ClinicManagementSystem.Features.Auth.Commands;
using ClinicManagementSystem.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

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
            IHttpContextAccessor httpContextAccessor)
        {
            _refreshTokenService = refreshTokenService;
            _userService = userService;
            _userRoleService = userRoleService;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // ⭐ Lấy refresh token từ Cookie
            var oldRefresh = _httpContextAccessor.HttpContext!.Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(oldRefresh))
                throw new UnauthorizedAccessException("Missing refresh token cookie");

            // ⭐ IPAddress luôn có giá trị hợp lệ
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString()
                     ?? "0.0.0.0";

            // ⭐ Validate refresh token
            var userId = await _refreshTokenService.ValidateRefreshToken(oldRefresh, ip);
            if (userId == null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            var user = await _userService.GetUserById(userId.Value);
            var roles = await _userRoleService.GetUserRoles(userId.Value);

            // ⭐ Generate new tokens
            var newAccess = _jwtService.GenerateAccessToken(user, roles);
            var newRefresh = _jwtService.GenerateRefreshToken();

            // ⭐ Rotate refresh token
            await _refreshTokenService.RevokeToken(oldRefresh, ip, newRefresh);
            await _refreshTokenService.SaveRefreshToken(user.UserId, newRefresh, ip);

            // ⭐ Update cookie
            _httpContextAccessor.HttpContext!.Response.Cookies.Append(
                "refreshToken",
                newRefresh,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(7)
                }
            );

            return new LoginResponseDto
            {
                AccessToken = newAccess,
                RefreshToken = newRefresh,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss")
            };
        }
    }
}
