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
            // ⭐ Lấy refresh token từ cookie
            var oldRefresh = _httpContextAccessor.HttpContext!.Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(oldRefresh))
                throw new UnauthorizedAccessException("Missing refresh token cookie");

            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();

            // ⭐ Validate refresh token
            var userId = await _refreshTokenService.ValidateRefreshToken(oldRefresh, ip);
            if (userId == null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            var user = await _userService.GetUserById(userId.Value);
            var roles = await _userRoleService.GetUserRoles(userId.Value);

            // ⭐ Generate NEW tokens
            var newAccess = _jwtService.GenerateAccessToken(user, roles);
            var newRefresh = _jwtService.GenerateRefreshToken();

            // ⭐ Rotation refresh token
            await _refreshTokenService.RevokeToken(oldRefresh, ip, newRefresh);
            await _refreshTokenService.SaveRefreshToken(user.UserId, newRefresh, ip);

            // ⭐ Set cookie mới
            _httpContextAccessor.HttpContext.Response.Cookies.Append(
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
                ExpiresAt = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss")
            };
        }
    }
}
