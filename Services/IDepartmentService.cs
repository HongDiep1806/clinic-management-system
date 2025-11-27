using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartments();
    }
}
