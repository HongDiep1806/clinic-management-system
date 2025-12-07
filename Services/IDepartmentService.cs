using ClinicManagementSystem.DTOs.Department;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllDepartments();
        Task<bool> DeleteDepartment(int id);
        Task<Department?> UpdateDepartment(Department dept);
        Task<Department?> CreateDepartment(Department model);
        Task<bool> RestoreDepartment(int id);
        Task<bool> ToggleDepartmentStatus(int id);
        //Task<List<dynamic>> GetAllDepartmentsWithStatus();

    }
}
