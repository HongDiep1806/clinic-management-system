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

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await _appointmentRepository.GetAll();
        }

        public async Task<Appointment> GetAppointmentById(int appointmentId)
        {
            return await _appointmentRepository.GetById(appointmentId);
        }

        public async Task<bool> UpdateAppointment(Appointment appointment, int appointmentId)
        {
            return await _appointmentRepository.Update(appointmentId, appointment);
        }
    }
}
