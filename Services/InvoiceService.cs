
using ClinicManagementSystem.DTOs.Invoice;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;

namespace ClinicManagementSystem.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository; 
        }

        public async Task<Invoice> CreateInvoice(Invoice invoice)
        {
            return await _invoiceRepository.Create(invoice);    
        }

        public async Task<List<Invoice>> GetPatientaInvoices(int patientId)
        {
           return await _invoiceRepository.GetPatientaInvoiceInclude(patientId);
        }

        public async Task<bool> IsInvoiceOfRecordExisted(int invoiceId)
        {
            return await _invoiceRepository.IsInvoiceOfRecordExisted(invoiceId);    
        }
    }
}
