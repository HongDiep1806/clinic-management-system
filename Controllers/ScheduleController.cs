using ClinicManagementSystem.DTOs.Schedule;
using ClinicManagementSystem.Features.Schedules.Commands;
using ClinicManagementSystem.Features.Schedules.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleDto dto)
        {
            try
            {
                var result = await _mediator.Send(new CreateScheduleCommand(dto));
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("doctor/{id}")]
        public async Task<IActionResult> GetDoctorSchedules(int id)
        {
            var result = await _mediator.Send(new GetDoctorSchedulesQuery(id));
            return Ok(result);
        }

    }
}
