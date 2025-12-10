namespace ClinicManagementSystem.DTOs.User
{
    public class UserStatusDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; }

        public string RoleName { get; set; }  // <--- Thêm vào đây nè

        public string Status { get; set; } // Active / Inactive
    }
}
