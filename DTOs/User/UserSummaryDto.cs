namespace ClinicManagementSystem.DTOs.User
{
    public class UserSummaryDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleName { get; set; }
        public string? DepartmentName { get; set; }
    }

}
