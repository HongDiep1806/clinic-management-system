using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Department Department { get; set; }

        [InverseProperty("Patient")]
        public ICollection<Appointment> AppointmentsAsPatient { get; set; }

        [InverseProperty("Doctor")]
        public ICollection<Appointment> AppointmentsAsDoctor { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }

    }

}
