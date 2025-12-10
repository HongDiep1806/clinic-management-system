namespace ClinicManagementSystem.DTOs.User
{
    public class EditUserDto
    {
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int? DepartmentId { get; set; }
        public string? NewPassword { get; set; }
        public string? Email { get; set; }  

    }
}
