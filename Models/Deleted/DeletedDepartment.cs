using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models.Deleted
{
    public class DeletedDepartment
    {
        [Key]
        public int DeletedDepartmentId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
