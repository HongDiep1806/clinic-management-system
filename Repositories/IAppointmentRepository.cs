using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<List<Appointment>> GetAllWithIncludes();
        Task<List<Appointment>> GetDoctorAppointments(int doctorId);
        Task<List<Appointment>> GetPatientAppointments(int patientId);

        // Kiểm tra bệnh nhân có lịch trong ngày chưa
        Task<bool> HasPatientAppointmentOnDate(int patientId, DateTime date);

        // Optional: nếu sau này muốn kiểm tra doctor trùng ngày thì giữ lại
        Task<bool> HasDoctorAppointmentOnDate(int doctorId, DateTime date);
    }
}
