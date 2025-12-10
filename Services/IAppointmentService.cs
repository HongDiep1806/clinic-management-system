using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IAppointmentService
    {
        Task<Appointment> CreateAppointment(Appointment appointment);
        Task<Appointment> CreateAppointmentByStaff(CreateAppointmentByStaffDto dto);

        Task<Appointment> GetAppointmentById(int appointmentId);
        Task<List<Appointment>> GetAllAppointments();
        Task<List<Appointment>> GetDoctorAppointments(int doctorId);
        Task<List<Appointment>> GetPatientAppointments(int patientId);

        Task<bool> UpdateAppointment(Appointment appointment, int appointmentId);
    }
}
