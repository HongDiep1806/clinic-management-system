using ClinicManagementSystem.Models.Deleted;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.DAL
{
    public class RestoreDbContext : DbContext
    {
        public RestoreDbContext(DbContextOptions<RestoreDbContext> options)
            : base(options) { }

        public DbSet<DeletedUser> DeletedUsers { get; set; }
        public DbSet<DeletedRole> DeletedRoles { get; set; }
        public DbSet<DeletedPermission> DeletedPermissions { get; set; }
        public DbSet<DeletedUserRole> DeletedUserRoles { get; set; }
        public DbSet<DeletedRolePermission> DeletedRolePermissions { get; set; }
        public DbSet<DeletedDepartment> DeletedDepartments { get; set; }
        public DbSet<DeletedAppointment> DeletedAppointments { get; set; }
        public DbSet<DeletedMedicalRecord> DeletedMedicalRecords { get; set; }
        public DbSet<DeletedInvoice> DeletedInvoices { get; set; }
        public DbSet<DeletedPrescription> DeletedPrescriptions { get; set; }
        public DbSet<DeletedMedicine> DeletedMedicines { get; set; }
        public DbSet<DeletedSchedule> DeletedSchedules { get; set; }
    }
}
