using ClinicManagementSystem.DTOs.Medicine;
using ClinicManagementSystem.Features.Medicines.Commands;
using ClinicManagementSystem.Features.Medicines.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Receptionist")]
    public class MedicineController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MedicineController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllMedicinesQuery());
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMedicineDto dto)
        {
            var result = await _mediator.Send(new CreateMedicineCommand(dto));
            return Ok(result);
        }
        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] UpdateMedicineDto dto)
        {
            var result = await _mediator.Send(new UpdateMedicineCommand(dto));
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteMedicineCommand(id));
            return result
                ? Ok(new { message = "Deleted successfully" })
                : NotFound(new { message = "Medicine not found" });
        }


    }
}
