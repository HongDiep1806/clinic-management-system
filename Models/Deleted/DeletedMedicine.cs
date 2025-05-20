using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models.Deleted
{
    public class DeletedMedicine
    {
        [Key]
        public int DeletedMedicineId { get; set; }
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int QuantityInStock { get; set; }
        public float Price { get; set; }
        public DateTime DeletedAt { get; set; } = DateTime.UtcNow;

    }
}
