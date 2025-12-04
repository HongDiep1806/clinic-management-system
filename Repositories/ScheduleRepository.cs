using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repositories
{
    public class ScheduleRepository: BaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Schedule> CreateScheduleForDoctor(Schedule schedule)
        {
            return await Create(schedule);
        }

        public async Task<Schedule?> GetDoctorScheduleAtTime(int doctorId, WeekDay day, TimeSpan time)
        {
            return await _context.Schedules
                .FirstOrDefaultAsync(s =>
                    s.DoctorId == doctorId &&
                    s.DayOfWeek == day &&
                    s.StartTime <= time && time <= s.EndTime);
        }

        public async Task<List<Schedule>> GetSchedulesByDoctorId(int doctorId)
        {
            return await _context.Schedules
                .Where(s => s.DoctorId == doctorId)
                .ToListAsync();
        }
        public async Task<bool> IsDoctorScheduleOverlapping(Schedule schedule)
        {
            return await _context.Schedules.AnyAsync(s =>
                s.DoctorId == schedule.DoctorId &&
                s.DayOfWeek == schedule.DayOfWeek &&
                s.StartTime < schedule.EndTime &&
                schedule.StartTime < s.EndTime
            );
        }
        public async Task<List<User>> GetDoctorsByWeekday(WeekDay weekday)
        {
            return await _context.Schedules
                .Where(s => s.DayOfWeek == weekday)
                .Include(s => s.Doctor)               
                    .ThenInclude(d => d.Schedules)    
                .Select(s => s.Doctor)
                .Distinct()
                .ToListAsync();
        }

        public async Task<Schedule?> GetDoctorScheduleAtDay(int doctorId, WeekDay day)
        {
            return await _context.Schedules
                .FirstOrDefaultAsync(s =>
                    s.DoctorId == doctorId &&
                    s.DayOfWeek == day
                );
        }



    }

}
