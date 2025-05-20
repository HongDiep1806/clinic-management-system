using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface  IAppointmentRepository: IBaseRepository<Appointment>
    {
        Task<List<Appointment>> GetAllWithIncludes();
        Task<List<Appointment>> GetDoctorAppointments(int doctorId, DateTime? date);
        Task<List<Appointment>> GetPatientAppointments(int patientId, DateTime? date);
    }
}
