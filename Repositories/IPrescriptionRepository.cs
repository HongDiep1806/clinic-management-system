using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IPrescriptionRepository:IBaseRepository<Prescription>
    {
        Task<List<Prescription>> GetByRecordId(int recordId);


    }

}
