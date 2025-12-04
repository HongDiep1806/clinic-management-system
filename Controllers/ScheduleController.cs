using ClinicManagementSystem.DTOs.Schedule;
using ClinicManagementSystem.Features.Schedules.Commands;
using ClinicManagementSystem.Features.Schedules.Queries;
using ClinicManagementSystem.Services;
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
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IMediator mediator, IScheduleService scheduleService )
        {
            _mediator = mediator;
            _scheduleService = scheduleService;
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
        [HttpDelete("delete/{scheduleId}")]
        public async Task<IActionResult> DeleteSchedule(int scheduleId)
        {
            var result = await _scheduleService.DeleteSchedule(scheduleId);

            if (!result)
                return NotFound(new { message = "Schedule not found" });

            return Ok(new { message = "Schedule deleted successfully" });
        }
        [HttpGet("doctors-by-weekday/{weekday}")]
        public async Task<IActionResult> GetDoctorsByWeekday(int weekday)
        {
            var doctors = await _scheduleService.GetDoctorsByWeekday(weekday);

            var result = doctors.Select(d => new {
                userId = d.UserId,
                fullName = d.FullName,
                email = d.Email,
                departmentId = d.DepartmentId,

                weekDays = d.Schedules != null
                    ? d.Schedules.Select(s => (int)s.DayOfWeek).ToList()
                    : new List<int>() 
            });

            return Ok(result);
        }






    }
}
