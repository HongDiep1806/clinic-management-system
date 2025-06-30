using ClinicManagementSystem.DTOs.Invoice;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IInvoiceService
    {
        Task<bool> IsInvoiceOfRecordExisted(int invoiceId);
        Task<Invoice> CreateInvoice(Invoice invoice);
        Task<List<Invoice>> GetPatientaInvoices(int patientId);
    }
}
