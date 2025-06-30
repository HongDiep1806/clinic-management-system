using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface  IAppointmentRepository: IBaseRepository<Appointment>
    {
        Task<List<Appointment>> GetAllWithIncludes();
        Task<List<Appointment>> GetDoctorAppointments(int doctorId);
        Task<List<Appointment>> GetPatientAppointments(int patientId);
        Task<bool> IsPatientAppointmentConflict(int patientId, DateTime dateTime);
        Task<bool> IsDoctorAppointmentConflict(int doctorId, DateTime dateTime);


    }
}
