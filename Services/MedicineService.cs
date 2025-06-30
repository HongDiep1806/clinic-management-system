
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;

namespace ClinicManagementSystem.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        public MedicineService(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<List<Medicine>> GetAllMedicines()
        {
            return await _medicineRepository.GetAll();
        }

        public Task<Medicine> GetMedicineById(int medicineId)
        {
            return _medicineRepository.GetById(medicineId);
        }
        public async Task<Medicine> CreateMedicine(Medicine medicine)
        {
            return await _medicineRepository.Create(medicine);
        }

        public async Task<bool> UpdateMedicine(int medicineId, Medicine updatedMedicine)
        {
            return await _medicineRepository.Update(medicineId, updatedMedicine);
        }

        public Task<bool> DeleteMedicine(int id)
        {
           return _medicineRepository.DeleteMedicine(id);
        }
    }
}
