using ClinicManagementSystem.DTOs.Prescription;
using ClinicManagementSystem.Features.Medicines.Queries;
using ClinicManagementSystem.Features.Prescriptions.Commands;
using ClinicManagementSystem.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [Authorize(Roles = "Admin, Doctor, Patient")]
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IMediator mediator, IPrescriptionService prescriptionService)
        {
            _mediator = mediator;
            _prescriptionService = prescriptionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePrescriptionDto dto)
        {
            var result = await _mediator.Send(new CreatePrescriptionCommand(dto));
            return Ok(result);
        }
        [HttpGet("record/{recordId}")]
        public async Task<IActionResult> GetByRecord(int recordId)
        {
            var result = await _prescriptionService.GetByRecordId(recordId);

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var result = await _prescriptionService.DeletePrescription(id);

            if (!result)
                return NotFound();

            return Ok(new { message = "Prescription deleted successfully" });
        }

    }

}
