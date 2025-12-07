using ClinicManagementSystem.DTOs.Department;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<List<Department>> GetAllDepartments();
        Task<bool> DeleteDepartment(int id);
        Task<Department?> UpdateDepartment(Department dept);
        Task<Department> CreateDepartment(Department dept);
        Task<bool> SafeDeleteDepartment(int id);
        Task<bool> RestoreDepartment(int id);
        Task<bool> ToggleDepartmentStatus(int id);
        Task<List<DepartmentDto>> GetAllDepartmentsWithStatus();

    }
}
