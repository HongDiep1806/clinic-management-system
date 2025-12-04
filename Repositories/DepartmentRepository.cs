using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Department> CreateDepartment(Department dept)
        {
            await _context.Departments.AddAsync(dept);
            await _context.SaveChangesAsync();
            return dept;  
        }


        public Task<List<Department>> GetAllDepartments()
        {
            return this._context.Departments.ToListAsync();
        }
        public async Task<Department?> UpdateDepartment(Department dept)
        {
            var existing = await _context.Departments.FindAsync(dept.DepartmentId);
            if (existing == null) return null;

            existing.Name = dept.Name;
            existing.Description = dept.Description;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null) return false;

            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SafeDeleteDepartment(int id)
        {
            var dept = await _context.Departments
                .Include(d => d.Users)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (dept == null) return false;

            if (dept.Users != null && dept.Users.Any())
                throw new InvalidOperationException("DEPARTMENT_HAS_USERS");

            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
