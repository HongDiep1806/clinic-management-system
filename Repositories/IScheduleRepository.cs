using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IScheduleRepository: IBaseRepository<Schedule>
    {
        Task<Schedule> CreateScheduleForDoctor(Schedule schedule);
        Task<List<Schedule>> GetSchedulesByDoctorId(int doctorId);
        Task<bool> IsDoctorScheduleOverlapping(Schedule schedule);
        Task<Schedule?> GetDoctorScheduleAtTime(int doctorId, WeekDay day, TimeSpan time);
    }
}
