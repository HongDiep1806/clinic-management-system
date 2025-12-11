using ClinicManagementSystem.DTOs.Auth.Login;
using ClinicManagementSystem.Features.Auth.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http; // ⭐ THÊM

namespace ClinicManagementSystem.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRoleService _userRoleService;
        private readonly IRefreshTokenService _refreshTokenService;

        public LoginCommandHandler(
            IUserService userService,
            IPasswordHasher<User> passwordHasher,
            IJwtService jwtService,
            IHttpContextAccessor httpContextAccessor,
            IUserRoleService userRoleService,
            IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleService = userRoleService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByEmail(request.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var checkPassword = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, request.Password);
            if (checkPassword == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid credentials");

            var roles = await _userRoleService.GetUserRoles(user.UserId);
            var accessToken = _jwtService.GenerateAccessToken(user, roles);
            var refreshToken = _jwtService.GenerateRefreshToken();

            var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            await _refreshTokenService.SaveRefreshToken(user.UserId, refreshToken, ip);

            // ⭐ Lưu refresh token vào cookie HttpOnly
            _httpContextAccessor.HttpContext!.Response.Cookies.Append(
                "refreshToken",
                refreshToken,
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
                AccessToken = accessToken,
                // ❗ KHÔNG trả refreshToken nữa!
                ExpiresAt = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss")
            };
        }
    }
}
