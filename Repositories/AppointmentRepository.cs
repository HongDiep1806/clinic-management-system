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

        public async Task<List<Appointment>> GetDoctorAppointments(int doctorId)
        {
            var appointments = await GetAllWithIncludes();

            appointments = appointments.Where(a => a.DoctorId == doctorId).ToList();

            return appointments;

        }

        public async Task<List<Appointment>> GetPatientAppointments(int patientId)
        {
            var appointments = await GetAllWithIncludes();

            appointments = appointments.Where(a => a.PatientId == patientId).ToList();

            return appointments;
        }

        public async Task<bool> IsPatientAppointmentConflict(int patientId, DateTime dateTime)
        {
            return await _context.Appointments.AnyAsync(a =>
                    a.PatientId == patientId &&
                    a.AppointmentDate == dateTime &&
                    a.Status != AppointmentStatus.Cancelled);
        }
        public static TimeSpan SlotDuration = TimeSpan.FromMinutes(30);

        public async Task<bool> IsDoctorAppointmentConflict(int doctorId, DateTime dateTime)
        {
            var sameDayAppointments = await _context.Appointments
                .Where(a =>
                    a.DoctorId == doctorId &&
                    a.Status != AppointmentStatus.Cancelled &&
                    a.AppointmentDate.Date == dateTime.Date)
                .ToListAsync();

            return sameDayAppointments.Any(a =>
                a.AppointmentDate <= dateTime &&
                dateTime < a.AppointmentDate + SlotDuration);
        }



    }

}
