using ClinicManagementSystem.DTOs.User;
using ClinicManagementSystem.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            var command = new RegisterUserCommand
            {
                CreateUserDto = createUserDto,
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
