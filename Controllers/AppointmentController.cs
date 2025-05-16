using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Features.Appointments.Commands;
using ClinicManagementSystem.Features.Appointments.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("get-all-appointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var query = new GetAllAppointmentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("book")]
        public async Task<IActionResult> Book([FromBody] BookAppointmentRequestDto dto)
        {
            var command = new BookAppointmentCommand(dto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateAppointmentStatusRequestDto dto)
        {
            var command = new UpdateAppointmentStatusCommand(dto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
