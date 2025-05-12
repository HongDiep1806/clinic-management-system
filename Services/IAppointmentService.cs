using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IAppointmentService
    {
        Task<Appointment> CreateAppointment(Appointment appointment);
    }
}
