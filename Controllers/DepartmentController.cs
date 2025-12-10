using ClinicManagementSystem.Features.Appointments.Queries;
using ClinicManagementSystem.Features.Departments.Queries;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IMediator mediator, IDepartmentService departmentService)
        {
            _mediator = mediator;
            _departmentService = departmentService;
        }
        [HttpGet("get-all-departments")]
        //[Authorize(Roles = "Admin, Receptionist")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var query = new GetAllDepartmentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department model)
        {
            if (id != model.DepartmentId)
                return BadRequest("Id mismatch");

            var updated = await _departmentService.UpdateDepartment(model);

            if (updated == null)
                return NotFound("Department not found");

            return Ok(updated);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var result = await _departmentService.DeleteDepartment(id);

                if (!result)
                    return NotFound("Department not found");

                return Ok(new { message = "Deleted successfully" });
            }
            catch (InvalidOperationException ex) when (ex.Message == "DEPARTMENT_HAS_USERS")
            {
                return BadRequest(new { error = "This department still has assigned doctors." });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDepartment([FromBody] Department model)
        {
            model.Users = new List<User>();

            var dept = await _departmentService.CreateDepartment(model);

            return Ok(dept);
        }
        [HttpPut("toggle-status/{id}")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var result = await _departmentService.ToggleDepartmentStatus(id);
                return Ok(new { success = result });
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message == "DEPARTMENT_HAS_ACTIVE_DOCTORS")
                    return Conflict(new { message = "DEPARTMENT_HAS_ACTIVE_DOCTORS" });

                return BadRequest(new { message = ex.Message });
            }
        }





    }
}
