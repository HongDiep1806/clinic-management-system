using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IMedicineRepository:IBaseRepository<Medicine>
    {
        Task<bool> DeleteMedicine(int medicineId);
    }
}
