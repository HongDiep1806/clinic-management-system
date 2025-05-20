using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IAppointmentService
    {
        Task<Appointment> CreateAppointment(Appointment appointment);
        Task<Appointment> GetAppointmentById(int appointmentId);
        Task<List<Appointment>> GetAllAppointments();
        Task<bool> UpdateAppointment (Appointment appointment, int appointmentId);
        Task<List<Appointment>> GetDoctorAppointments(int doctorId, DateTime? date);
        Task<List<Appointment>> GetPatientAppointments(int patientId, DateTime? date);

    }
}
