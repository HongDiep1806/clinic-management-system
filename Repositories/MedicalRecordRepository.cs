using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ClinicManagementSystem.Repositories
{
    public class MedicalRecordRepository : BaseRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<MedicalRecord> GetMedicalRecordByIdIncludePres(int recordId)
        {
            var record = await _context.MedicalRecords
                .Include(a => a.Appointment)
                .Include(r => r.Prescriptions)
                .ThenInclude(p => p.Medicine)
                .FirstOrDefaultAsync(r => r.MedicalRecordId == recordId);
            return record;
        }

        public async Task<List<MedicalRecord>> GetMedicalRecordsByPatientId(int patientId)
        {
            var record = await _context.MedicalRecords
                .Include(a => a.Appointment)
                .Where(r => r.Appointment.PatientId == patientId)
                .Include(r => r.Prescriptions)
                .ThenInclude(p => p.Medicine)
                .ToListAsync();
            return record;
        }
    }
}
