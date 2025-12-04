using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; 


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

        [JsonIgnore] // tránh serialize navigation ngược
        public Department Department { get; set; }

        [JsonIgnore]
        [InverseProperty("Patient")]
        public ICollection<Appointment>? AppointmentsAsPatient { get; set; }

        [JsonIgnore]
        [InverseProperty("Doctor")]
        public ICollection<Appointment>? AppointmentsAsDoctor { get; set; }

        [JsonIgnore]
        public ICollection<Schedule> Schedules { get; set; }

        [JsonIgnore]
        public ICollection<UserRole> UserRoles { get; set; }

        [JsonIgnore]
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }

}
