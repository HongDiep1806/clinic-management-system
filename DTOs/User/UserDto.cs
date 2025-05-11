namespace ClinicManagementSystem.DTOs.User
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public DateTime Dob { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public int? DepartmentId { get; set; }

        public string DepartmentName { get; set; } 

        public List<string> Roles { get; set; }   
    }
}
