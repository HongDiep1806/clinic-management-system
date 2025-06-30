
using ClinicManagementSystem.DTOs.Medicine;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IMedicineService
    {
        Task<Medicine> GetMedicineById(int medicineId);
        Task<List<Medicine>> GetAllMedicines();
        Task<Medicine> CreateMedicine(Medicine medicine);
        Task<bool> UpdateMedicine(int medicineId, Medicine updatedMedicine);
        Task<bool> DeleteMedicine(int id);
    }
}
    