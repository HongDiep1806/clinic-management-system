using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;

namespace ClinicManagementSystem.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public ScheduleService(IScheduleRepository scheduleRepository, IUserRoleRepository userRoleRepository)
        {
            _scheduleRepository = scheduleRepository;
            _userRoleRepository = userRoleRepository;
        }
        public async Task<Schedule> CreateSchedule(Schedule schedule)
        {
            await EnsureUserIsDoctor(schedule.DoctorId);

            if (await IsDoctorScheduleOverlapping(schedule))
                throw new InvalidOperationException("Schedule overlaps with existing one.");

            return await _scheduleRepository.CreateScheduleForDoctor(schedule);
        }

        public async Task<List<Schedule>> GetSchedulesByDoctorId(int doctorId)
        {
            await EnsureUserIsDoctor(doctorId);
            return await _scheduleRepository.GetSchedulesByDoctorId(doctorId);
        }


        public Task<bool> IsDoctorScheduleOverlapping(Schedule schedule)
        {
            return _scheduleRepository.IsDoctorScheduleOverlapping(schedule);
        }
        private async Task EnsureUserIsDoctor(int userId)
        {
            var userRoles = await _userRoleRepository.GetUserRoles(userId);
            if (userRoles == null || !userRoles.Any(r => r.ToLower() == "doctor"))
                throw new InvalidOperationException("The given user is not a doctor.");
        }


    }
}
