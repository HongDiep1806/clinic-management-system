using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(u => u.AppointmentsAsDoctor)
                .HasForeignKey(a => a.DoctorId)
                .IsRequired(false)                         // 🔥 now nullable
                .OnDelete(DeleteBehavior.NoAction);        // 🔥 prevent EF from blocking deletion

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(u => u.AppointmentsAsPatient)
                .HasForeignKey(a => a.PatientId)
                .IsRequired(false)                         // 🔥 now nullable
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<User>()
                .HasMany(u => u.RefreshTokens)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion(
                  v => v.ToString(),
                  v => (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), v));
            modelBuilder.Entity<Invoice>()
                .Property(i => i.PaymentMethod)
                .HasConversion(
                    v => v.ToString(),
                    v => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), v));
            modelBuilder.Entity<Schedule>()
                .Property(s => s.DayOfWeek)
                .HasConversion<string>();



        }

    }

}
