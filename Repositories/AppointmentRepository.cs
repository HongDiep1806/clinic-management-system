using Azure.Core;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClinicManagementSystem.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Appointment>> GetAllWithIncludes()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.MedicalRecords)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetDoctorAppointments(int doctorId, DateTime? date)
        {
            var appointments = await GetAllWithIncludes();
            if (date.HasValue)
            {
                var targetDate = date.Value.Date;
                appointments = appointments.Where(a => a.AppointmentDate == targetDate && a.DoctorId == doctorId).ToList();
            }
            return appointments;

        }

        public async Task<List<Appointment>> GetPatientAppointments(int patientId, DateTime? date)
        {
            var appointments = await GetAllWithIncludes();
            if (date.HasValue)
            {
                var targetDate = date.Value.Date;
                appointments = appointments.Where(a => a.AppointmentDate == targetDate && a.PatientId == patientId).ToList();
            }
            return appointments;
        }
    }

}
