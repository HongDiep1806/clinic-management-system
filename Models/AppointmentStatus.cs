namespace ClinicManagementSystem.Models
{
    public enum AppointmentStatus
    {
        Pending = 0,      // Đặt online hoặc receptionist tạo – chưa đến
        Confirmed = 1,    // Đến check-in quầy
        Completed = 2,    // Khám xong
        Cancelled = 3,    // Hủy bởi bệnh nhân hoặc nhân viên
        NoShow = 4        // Không đến
    }
}
