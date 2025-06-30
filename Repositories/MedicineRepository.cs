using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Models.Deleted;

namespace ClinicManagementSystem.Repositories
{
    public class MedicineRepository:BaseRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(ApplicationDbContext context, RestoreDbContext restoreContext) : base(context, restoreContext)
        {
        }

        public async Task<bool> DeleteMedicine(int medicineId)
        {
            var medicine = await GetById(medicineId);
            if (medicine == null)
            {
                return false;
            }
            var deletedMedicine = new DeletedMedicine
            {
                MedicineId = medicine.MedicineId,
                Name = medicine.Name,
                Unit = medicine.Unit,
                QuantityInStock = medicine.QuantityInStock,
                Price = medicine.Price,
                DeletedAt = DateTime.UtcNow
            };
            await Delete(deletedMedicine.MedicineId);
            await _restoreContext.DeletedMedicines.AddAsync(deletedMedicine);
            await _restoreContext.SaveChangesAsync();
            return true;

        }
    }   

}
