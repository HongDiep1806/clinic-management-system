using ClinicManagementSystem.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);

            // ⭐ Set refreshToken cookie
            Response.Cookies.Append(
                "refreshToken",
                result.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(7)
                }
            );

            // ⭐ FE chỉ nhận 2 field này
            return Ok(new
            {
                accessToken = result.AccessToken,
                expiresAt = result.ExpiresAt
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized(new { message = "Missing refresh token" });

            var command = new RefreshTokenCommand(refreshToken); 

            var result = await _mediator.Send(command);


            // ⭐ Rotate cookie
            Response.Cookies.Append(
                "refreshToken",
                result.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(7)
                }
            );

            return Ok(new
            {
                accessToken = result.AccessToken,
                expiresAt = result.ExpiresAt
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
        {
            await _mediator.Send(command);

            // ⭐ Delete refresh cookie
            Response.Cookies.Delete("refreshToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok(new { message = "Logged out successfully." });
        }
    }
}

//using ClinicManagementSystem.Features.Auth.Commands;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace ClinicManagementSystem.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly IMediator _mediator;

//        public AuthController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginCommand command)
//        {
//            var result = await _mediator.Send(command);
//            return Ok(result);
//        }
//        [HttpPost("logout")]
//        public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
//        {
//            await _mediator.Send(command);
//            return Ok(new { message = "Logged out successfully." });
//        }
//        [HttpPost("refresh")]
//        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
//        {
//            var result = await _mediator.Send(command);
//            return Ok(result);
//        }

//    }
//}
