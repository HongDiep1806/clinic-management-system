using Azure.Core;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ClinicManagementSystem.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Invoice>> GetPatientaInvoiceInclude(int patientId)
        {
            var invoices = await _context.Invoices
                                .Where(i => i.Record.Appointment.PatientId == patientId)
                                .Include(i => i.Record)
                                .ThenInclude(r => r.Appointment)
                                .ToListAsync();
            return invoices;
        }

        public async Task<bool> IsInvoiceOfRecordExisted(int recordId)
        {
            return await _context.Invoices
                                .AnyAsync(i => i.Record.MedicalRecordId == recordId);
        }
    }
}
