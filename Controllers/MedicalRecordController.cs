using ClinicManagementSystem.DTOs.MedicalRecord;
using ClinicManagementSystem.Features.MedicalRecords.Commands;
using ClinicManagementSystem.Features.MedicalRecords.Queries;
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

        public MedicalRecordsController(IMediator mediator)
        {
            _mediator = mediator;
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


    }
}
