using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Features.Appointments.Commands;
using ClinicManagementSystem.Features.Appointments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var query = new GetAllAppointmentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("book")]
        [Authorize(Roles = "Patient, Admin")]
        public async Task<IActionResult> Book([FromBody] BookAppointmentRequestDto dto)
        {
            var command = new BookAppointmentCommand(dto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut("update-status")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateAppointmentStatusRequestDto dto)
        {
            var command = new UpdateAppointmentStatusCommand(dto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("doctor-appointments")]
        [Authorize(Roles = "Doctor")]   
        public async Task<IActionResult> GetAppointmentsForDoctor([FromQuery] DoctorAppointmentsRequestDto doctorAppointmentsRequestDto)
        {
            var result = await _mediator.Send(new GetDoctorAppointmentsQuery(doctorAppointmentsRequestDto));
            return Ok(result);
        }
        [HttpGet("patient-appointments")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetAppointmentsForPatient([FromQuery] PatientAppointmentsRequestDto patientAppointmentsRequestDto)
        {
            var result = await _mediator.Send(new GetPatientAppointmentsQuery(patientAppointmentsRequestDto));
            return Ok(result);
        }
        [HttpPut("{id}/cancel")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Cancel(int id)
        {
            var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _mediator.Send(new CancelAppointmentCommand(id, patientId));
            return result ? Ok(new { message = "Appointment cancelled." }) : BadRequest();
        }

    }
}
