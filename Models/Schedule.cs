namespace ClinicManagementSystem.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int DoctorId { get; set; }
        public WeekDay DayOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string RoomNumber { get; set; }

        public User Doctor { get; set; }
    }

}
