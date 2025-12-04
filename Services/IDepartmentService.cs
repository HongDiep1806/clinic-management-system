using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartments();
        Task<bool> DeleteDepartment(int id);
        Task<Department?> UpdateDepartment(Department dept);
        Task<Department?> CreateDepartment(Department model);

    }
}
