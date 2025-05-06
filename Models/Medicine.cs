namespace ClinicManagementSystem.Models
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int QuantityInStock { get; set; }
        public float Price { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
