using ClinicManagementSystem.DTOs.Invoice;
using ClinicManagementSystem.Features.Invoices.Commands;
using ClinicManagementSystem.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClinicManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceDto dto)
        {
            var result = await _mediator.Send(new CreateInvoiceCommand(dto));
            return Ok(result);
        }
        [HttpGet("my-invoices")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetMyInvoices()
        {
            var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _mediator.Send(new GetInvoicesByPatientQuery(patientId));
            return Ok(result);
        }
    }

}
