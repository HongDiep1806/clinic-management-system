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
            return await _context.MedicalRecords
                .AsNoTracking()
                .Include(a => a.Appointment)
                .Include(r => r.Prescriptions)
                .ThenInclude(p => p.Medicine)
                .FirstOrDefaultAsync(r => r.MedicalRecordId == recordId);
        }

        public async Task<List<MedicalRecord>> GetMedicalRecordsByPatientId(int patientId)
        {
            return await _context.MedicalRecords
                .AsNoTracking()

                .Where(r => r.Appointment.PatientId == patientId)
                .Include(r => r.Appointment)
                    .ThenInclude(a => a.Doctor)

                .Include(r => r.Appointment)
                    .ThenInclude(a => a.Patient)

                .Include(r => r.Prescriptions)
                    .ThenInclude(p => p.Medicine)

                .ToListAsync();
        }
        public async Task<MedicalRecord> GetMedicalRecordByAppointmentId(int appointmentId)
        {
            return await _context.MedicalRecords
     .AsNoTracking()
     .Include(r => r.Appointment)
         .ThenInclude(a => a.Doctor)
     .Include(r => r.Appointment)
         .ThenInclude(a => a.Patient)
     .FirstOrDefaultAsync(r => r.AppointmentId == appointmentId);
        }
        public async Task<List<MedicalRecord>> GetMedicalRecordsByDoctorId(int doctorId)
        {
            return await _context.MedicalRecords
                .Include(r => r.Appointment)
                    .ThenInclude(a => a.Patient)
                .Include(r => r.Prescriptions)
                    .ThenInclude(p => p.Medicine)
                .Where(r => r.Appointment.DoctorId == doctorId)
                .ToListAsync();
        }
    }
}
