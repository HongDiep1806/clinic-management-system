using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

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
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetDoctorAppointments(int doctorId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetPatientAppointments(int patientId)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        }

        // Kiểm tra bệnh nhân đặt trùng ngày
        public async Task<bool> HasPatientAppointmentOnDate(int patientId, DateTime date)
        {
            return await _context.Appointments.AnyAsync(a =>
                a.PatientId == patientId &&
                a.Date.Date == date.Date &&
                a.Status != AppointmentStatus.Cancelled
            );
        }

        // Kiểm tra bác sĩ đã có lịch làm không (service sẽ gọi ScheduleRepository)
        public async Task<bool> HasDoctorAppointmentOnDate(int doctorId, DateTime date)
        {
            return await _context.Appointments.AnyAsync(a =>
                a.DoctorId == doctorId &&
                a.Date.Date == date.Date &&
                a.Status != AppointmentStatus.Cancelled
            );
        }
    }
}
