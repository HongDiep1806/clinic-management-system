using ClinicManagementSystem.DTOs.User;
using ClinicManagementSystem.Features.Users.Commands;
using ClinicManagementSystem.Features.Users.Queries;
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
        [HttpGet("get-all-patients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var query = new GetAllPatientsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("get-all-doctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var query = new GetAllDoctorsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("get-users-by-role")]
        public async Task<IActionResult> GetUsersByRole([FromQuery] string roleName)
        {
            var result = await _mediator.Send(new GetUsersByRoleQuery(roleName));
            return Ok(result);
        }
    }
}
