using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;

namespace ClinicManagementSystem.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<List<Department>> GetAllDepartments()
        {
            return await _departmentRepository.GetAllDepartments();
        }
        public async Task<Department?> UpdateDepartment(Department dept)
        {
            return await _departmentRepository.UpdateDepartment(dept);
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            return await _departmentRepository.SafeDeleteDepartment(id);
        }
        public async Task<Department?> CreateDepartment(Department model)
        {
            return await _departmentRepository.CreateDepartment(model);
        }

    }
}
