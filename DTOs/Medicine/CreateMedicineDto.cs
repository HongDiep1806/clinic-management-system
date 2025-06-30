namespace ClinicManagementSystem.DTOs.Medicine
{
    public class CreateMedicineDto
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public int QuantityInStock { get; set; }
        public float Price { get; set; }
    }

}
