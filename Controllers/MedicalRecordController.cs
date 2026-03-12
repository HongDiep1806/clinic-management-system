using ClinicManagementSystem.DTOs.MedicalRecord;
using ClinicManagementSystem.Features.MedicalRecords.Commands;
using ClinicManagementSystem.Features.MedicalRecords.Queries;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClinicManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordsController(IMediator mediator, IMedicalRecordService medicalRecordService)
        {
            _mediator = mediator;
            _medicalRecordService = medicalRecordService;
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]

        public async Task<IActionResult> Create([FromBody] CreateMedicalRecordDto dto)
        {
            var result = await _mediator.Send(new CreateMedicalRecordCommand(dto));
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetMedicalRecordByIdQuery(id));
            return Ok(result);
        }
        [HttpGet("my-medical-records")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetMyRecords()
        {
            var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _mediator.Send(new GetMedicalRecordsByPatientQuery(patientId));
            return Ok(result);
        }
        [HttpGet("doctor/my-records")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetDoctorMedicalRecords()
        {
            var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var records = await _medicalRecordService.GetMedicalRecordsByDoctorId(doctorId);

            return Ok(records);
        }
        [HttpGet("appointment/{appointmentId}")]
        [Authorize]
        public async Task<IActionResult> GetByAppointmentId(int appointmentId)
        {
            var result = await _medicalRecordService.GetMedicalRecordByAppointmentId(appointmentId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("appointment/{appointmentId}/prescriptions")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetPrescriptionsByAppointment(int appointmentId)
        {
            var record = await _medicalRecordService.GetMedicalRecordByAppointmentId(appointmentId);

            if (record == null)
                return NotFound();

            var recordFull = await _medicalRecordService.GetMedicalRecordByIdIncludePres(record.MedicalRecordId);

            return Ok(recordFull.Prescriptions);
        }
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetMedicalRecordById(int id)
        {
            var record = await _medicalRecordService.GetMedicalRecordByIdIncludePres(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalRecord(int id, [FromBody] UpdateMedicalRecordDto dto)
        {
            var record = await _medicalRecordService.GetMedicalRecordById(id);

            if (record == null)
                return NotFound();

            record.DiagnosisDescription = dto.DiagnosisDescription;
            record.Treatment = dto.Treatment;
            record.Note = dto.Note;

            var result = await _medicalRecordService.UpdateMedicalRecord(id, record);

            if (!result)
                return BadRequest();

            return Ok(record);
        }

    }
}
