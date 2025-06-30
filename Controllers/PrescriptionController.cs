using ClinicManagementSystem.DTOs.Prescription;
using ClinicManagementSystem.Features.Medicines.Queries;
using ClinicManagementSystem.Features.Prescriptions.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [Authorize(Roles = "Admin, Doctor")]
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrescriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePrescriptionDto dto)
        {
            var result = await _mediator.Send(new CreatePrescriptionCommand(dto));
            return Ok(result);
        }
        
    }

}
