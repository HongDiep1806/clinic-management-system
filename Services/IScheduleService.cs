using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IScheduleService
    {
        Task<Schedule> CreateSchedule(Schedule schedule);
        Task<List<Schedule>> GetSchedulesByDoctorId(int doctorId);
        Task<bool> IsDoctorScheduleOverlapping(Schedule schedule);

    }
}
