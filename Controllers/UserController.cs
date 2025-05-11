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

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] CreateUserDto createUserDto)
        {
            createUserDto.DepartmentId = null;
            createUserDto.RoleId = 1;
            var command = new RegisterUserCommand
            {
                CreateUserDto = createUserDto,
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] CreateUserDto createUserDto)
        {
            createUserDto.DepartmentId = null;
            createUserDto.RoleId = 2;
            var command = new RegisterUserCommand
            {
                CreateUserDto = createUserDto,
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("register-receptionist")]
        public async Task<IActionResult> RegisterReceptionist([FromBody] CreateUserDto createUserDto)
        {
            createUserDto.DepartmentId = null;
            createUserDto.RoleId = 3;
            var command = new RegisterUserCommand
            {
                CreateUserDto = createUserDto,
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("register-doctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] CreateUserDto createUserDto)
        {
            createUserDto.RoleId = 4;
            var command = new RegisterUserCommand
            {
                CreateUserDto = createUserDto,
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
