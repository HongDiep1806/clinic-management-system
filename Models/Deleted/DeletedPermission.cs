using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models.Deleted
{
    public class DeletedPermission
    {
        [Key]
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
    }
}
