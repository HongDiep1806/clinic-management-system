using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models.Deleted
{
    public class DeletedRole
    {
        [Key]
        public int DeletedRoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
