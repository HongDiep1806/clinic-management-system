using ClinicManagementSystem.DTOs.Department;
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
        public async Task<List<DepartmentDto>> GetAllDepartments()
        {
            return await _departmentRepository.GetAllDepartmentsWithStatus();
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

        public async Task<bool> RestoreDepartment(int id)
        {
            return await _departmentRepository.RestoreDepartment(id);
        }

        public async Task<bool> ToggleDepartmentStatus(int id)
        {
            return await _departmentRepository.ToggleDepartmentStatus(id);
        }
    }
}
