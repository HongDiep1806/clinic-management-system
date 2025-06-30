namespace ClinicManagementSystem.DTOs.Schedule
{
    public class ScheduleDto
    {
        public int ScheduleId { get; set; }
        public int DoctorId { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string RoomNumber { get; set; }
    }


}
