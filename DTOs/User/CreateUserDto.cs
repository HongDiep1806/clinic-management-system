namespace ClinicManagementSystem.DTOs.User
{
    public class CreateUserDto
    {
        public string FullName { get; set; }

        public DateTime Dob { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int? DepartmentId { get; set; } = null;

        public int RoleId { get; set; } 
    }
}
