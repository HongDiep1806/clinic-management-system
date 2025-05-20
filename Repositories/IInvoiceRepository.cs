using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IInvoiceRepository:IBaseRepository<Invoice>
    {
        Task<bool> IsInvoiceOfRecordExisted(int recordId);
        Task<List<Invoice>> GetPatientaInvoiceInclude(int patientId);
    }
}
