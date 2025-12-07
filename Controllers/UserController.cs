using ClinicManagementSystem.DTOs.User;
using ClinicManagementSystem.Features.Users.Commands;
using ClinicManagementSystem.Features.Users.Queries;
using ClinicManagementSystem.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Doctor,Admin,Receptionist")]

    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IUserService _userService;

        public UserController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
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
        //[HttpGet("get-all-patients")]
        //public async Task<IActionResult> GetAllPatients()
        //{
        //    var query = new GetAllPatientsQuery();
        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}
        //[HttpGet("get-all-doctors")]
        //public async Task<IActionResult> GetAllDoctors()
        //{
        //    var query = new GetAllDoctorsQuery();
        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}
        [HttpGet("get-users-by-role")]
        public async Task<IActionResult> GetUsersByRole([FromQuery] string roleName)
        {
            var result = await _mediator.Send(new GetUsersByRoleQuery(roleName));
            return Ok(result);
        }
        [HttpPut("edit/{userId}")]
        public async Task<IActionResult> EditUser(int userId, [FromBody] EditUserDto dto)
        {
            var updated = await _userService.EditUser(userId, dto);
            return Ok(updated);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);

            if (!result)
                return NotFound("User not found");

            return Ok("User deleted successfully");
        }
        [HttpGet("get-all-doctors")]
        public async Task<IActionResult> GetDoctors()
        {
            var result = await _userService.GetAllDoctorsWithStatus();
            return Ok(result);
        }

        [HttpGet("get-all-patients")]
        public async Task<IActionResult> GetPatients()
        {
            var result = await _userService.GetAllPatientsWithStatus();
            return Ok(result);
        }
        [HttpPut("toggle-status/{id}")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _userService.ToggleUserStatus(id);

            if (!result)
                return NotFound("User not found");

            return Ok("Status toggled successfully");
        }
        [HttpPut("restore-with-email/{id}")]
        public async Task<IActionResult> RestoreWithEmail(
    int id,
    [FromBody] RestoreEmailDto dto)
        {
            try
            {
                await _userService.RestoreUserWithNewEmail(id, dto.NewEmail);
                return Ok(true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_IN_USE"))
                    return Conflict("EMAIL_IN_USE");

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-user/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdWithStatus(userId);

            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }
        [HttpGet("get-all-staffs")]
        public async Task<IActionResult> GetAllReceptionists()
        {
            var result = await _userService.GetAllReceptionistsWithStatus();
            return Ok(result);
        }
        [HttpPut("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword(int userId, [FromBody] ChangePasswordDto dto)
        {
            try
            {
                var result = await _userService.ChangePassword(userId, dto.CurrentPassword, dto.NewPassword);

                if (!result)
                    return NotFound(new { message = "User not found" });

                return Ok(new { message = "Password changed successfully" });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("INVALID_CURRENT_PASSWORD"))
                    return BadRequest(new { message = "Current password is incorrect" });

                return BadRequest(new { message = ex.Message });
            }
        }








    }
}
