using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;

namespace ClinicManagementSystem.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository; 
        }
        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
           return await _appointmentRepository.Create(appointment);
        }
    }
}
