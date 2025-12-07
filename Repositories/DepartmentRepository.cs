using ClinicManagementSystem.DAL;
using ClinicManagementSystem.DTOs.Department;
using ClinicManagementSystem.Models.Deleted;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(
        ApplicationDbContext context,
        RestoreDbContext restoreContext,
        IPasswordHasher<User> passwordHasher)
        : base(context, restoreContext)
    {
    }

    public async Task<Department> CreateDepartment(Department dept)
    {
        await _context.Departments.AddAsync(dept);
        await _context.SaveChangesAsync();
        return dept;
    }

    public Task<List<Department>> GetAllDepartments()
        => _context.Departments.ToListAsync();

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
                .ThenInclude(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(d => d.DepartmentId == id);

        if (dept == null)
            throw new InvalidOperationException("DEPARTMENT_NOT_FOUND");

        // Nếu Users null -> set list rỗng
        var users = dept.Users ?? new List<User>();

        // Lấy doctor
        var doctorIds = users
            .Where(u => u.UserRoles != null &&
                        u.UserRoles.Any(ur => ur.Role != null && ur.Role.RoleName == "Doctor"))
            .Select(u => u.UserId)
            .ToList();

        // Không có doctor → cho deactivate
        if (!doctorIds.Any())
        {
            await DeactivateDepartmentRecord(dept);
            return true;
        }

        // Lấy doctor inactive từ restore DB
        var inactiveDoctorIds = await _restoreContext.DeletedUsers
            .Where(d => d.RoleName == "Doctor" && doctorIds.Contains(d.UserId))
            .Select(d => d.UserId)
            .ToListAsync();

        if (inactiveDoctorIds.Count != doctorIds.Count)
            throw new InvalidOperationException("DEPARTMENT_HAS_ACTIVE_DOCTORS");

        await DeactivateDepartmentRecord(dept);
        return true;
    }

    public async Task<bool> ToggleDepartmentStatus(int id)
    {
        var deletedRecord = await _restoreContext.DeletedDepartments
            .FirstOrDefaultAsync(d => d.DepartmentId == id);

        if (deletedRecord != null)
        {
            _restoreContext.DeletedDepartments.Remove(deletedRecord);
            await _restoreContext.SaveChangesAsync();
            return true; // restored
        }

        var success = await SafeDeleteDepartment(id);
        return success;
    }

    private async Task DeactivateDepartmentRecord(Department dept)
    {
        var deleted = new DeletedDepartment
        {
            DepartmentId = dept.DepartmentId,
            Name = dept.Name,
            Description = dept.Description
        };

        await _restoreContext.DeletedDepartments.AddAsync(deleted);
        await _restoreContext.SaveChangesAsync();
    }

    public async Task<bool> RestoreDepartment(int id)
    {
        var deleted = await _restoreContext.DeletedDepartments
            .FirstOrDefaultAsync(x => x.DepartmentId == id);
        if (deleted == null)
            return false;

        _restoreContext.DeletedDepartments.Remove(deleted);
        await _restoreContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<DepartmentDto>> GetAllDepartmentsWithStatus()
    {
        var active = await _context.Departments.ToListAsync();

        var deletedIds = await _restoreContext.DeletedDepartments
            .Select(d => d.DepartmentId)
            .ToListAsync();

        return active.Select(d => new DepartmentDto
        {
            DepartmentId = d.DepartmentId,
            Name = d.Name,
            Description = d.Description,
            Status = deletedIds.Contains(d.DepartmentId) ? "Inactive" : "Active"
        }).ToList();
    }

}
