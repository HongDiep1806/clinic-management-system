using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<List<Department>> GetAllDepartments();

    }
}
