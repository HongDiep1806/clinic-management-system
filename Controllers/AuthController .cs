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

            // set cookie từ refreshToken handler trả về
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

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var result = await _mediator.Send(new RefreshTokenCommand());

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
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutCommand());

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
