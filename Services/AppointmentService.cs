using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;

namespace ClinicManagementSystem.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUserRepository _userRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository,  IScheduleRepository scheduleRepository, IUserRepository userRepository)
        {
            _appointmentRepository = appointmentRepository;
            _scheduleRepository = scheduleRepository;
            _userRepository = userRepository;
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            await ValidateAppointment(appointment);
            await SnapshotDepartment(appointment);   // << THÊM DÒNG NÀY
            return await _appointmentRepository.Create(appointment);
        }


        public async Task<Appointment> CreateAppointmentByStaff(CreateAppointmentByStaffDto dto)
        {
            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                Date = dto.Date,
                Reason = dto.Reason,
                Status = AppointmentStatus.Pending, // staff can also create pending
                CreatedAt = DateTime.Now
            };

            await ValidateAppointment(appointment);
            return await _appointmentRepository.Create(appointment);
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            //await SyncExpiredAppointments();
            return await _appointmentRepository.GetAllWithIncludes();
        }

        public async Task<Appointment> GetAppointmentById(int appointmentId)
        {
            return await _appointmentRepository.GetById(appointmentId);
        }

        public async Task<List<Appointment>> GetDoctorAppointments(int doctorId)
        {
            await SyncExpiredAppointments();
            return await _appointmentRepository.GetDoctorAppointments(doctorId);
        }

        public async Task<List<Appointment>> GetPatientAppointments(int patientId)
        {
            await SyncExpiredAppointments();
            return await _appointmentRepository.GetPatientAppointments(patientId);
        }

        public async Task<bool> UpdateAppointment(Appointment appointment, int appointmentId)
        {
            return await _appointmentRepository.Update(appointmentId, appointment);
        }

        private async Task ValidateAppointment(Appointment appointment)
        {
            var date = appointment.Date;

            if (appointment.DoctorId == null)
                throw new InvalidOperationException("Doctor is required.");

            if (appointment.PatientId == null)
                throw new InvalidOperationException("Patient is required.");

            int doctorId = appointment.DoctorId.Value;
            int patientId = appointment.PatientId.Value;

            // 1. Doctor must work on this day
            var weekDay = (WeekDay)(((int)date.DayOfWeek + 6) % 7);

            var schedule = await _scheduleRepository
                .GetDoctorScheduleAtDay(doctorId, weekDay);

            if (schedule == null)
                throw new InvalidOperationException("Doctor is not scheduled to work on this day.");

            // 2. Patient cannot book 2 appointments on same day
            bool hasPatientConflict = await _appointmentRepository.HasPatientAppointmentOnDate(
                patientId,
                date
            );

            if (hasPatientConflict)
                throw new InvalidOperationException("Patient already has an appointment on this day.");
        }
        private async Task SnapshotDepartment(Appointment appointment)
        {
            if (appointment.DoctorId == null)
                throw new InvalidOperationException("Doctor is required.");

            // Lấy doctor + department từ UserRepository
            var doctor = await _userRepository.GetByIdWithDepartment(appointment.DoctorId.Value);

            if (doctor == null)
                throw new InvalidOperationException("Doctor not found.");

            appointment.DepartmentId = doctor.DepartmentId ?? 0;
            appointment.DepartmentName = doctor.Department?.Name ?? "Unknown";
        }
        public async Task SyncExpiredAppointments()
        {
            var today = DateTime.Today;

            var allAppointments = await _appointmentRepository.GetAllWithIncludes();

            var expiredAppointments = allAppointments
                .Where(a =>
                    (a.Status == AppointmentStatus.Pending ||
                     a.Status == AppointmentStatus.Confirmed)
                    && a.Date.Date < today)
                .ToList();

            foreach (var appointment in expiredAppointments)
            {
                appointment.Status = AppointmentStatus.NoShow;
                await _appointmentRepository.Update(appointment.AppointmentId, appointment);
            }
        }
        public async Task<bool> ConfirmArrival(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetById(appointmentId);
            if (appointment == null)
                throw new InvalidOperationException("Appointment not found.");

            ApplyArrivalConfirmationPolicy(appointment, DateTime.Now);

            return await _appointmentRepository.Update(appointmentId, appointment);
        }

        private void ApplyArrivalConfirmationPolicy(Appointment appointment, DateTime now)
        {
            // Chỉ confirm được khi đang Pending
            if (appointment.Status != AppointmentStatus.Pending)
                throw new InvalidOperationException("Only pending appointments can be confirmed.");

            // Nếu đã qua ngày -> NoShow
            if (appointment.Date.Date < now.Date)
            {
                appointment.Status = AppointmentStatus.NoShow;
                return;
            }

            // Nếu chưa tới ngày -> không cho confirm
            if (appointment.Date.Date > now.Date)
                throw new InvalidOperationException("Cannot confirm arrival before appointment date.");

            // ===== Đúng ngày: check cutoff theo ca =====
            // Ca suy từ giờ đại diện bạn lưu: 08:00 (morning) / 13:00 (afternoon)
            bool isMorning = appointment.Date.Hour < 12;

            // Cutoff đề xuất: 11:30 / 16:30
            DateTime cutoff = isMorning
                ? appointment.Date.Date.AddHours(11).AddMinutes(30)
                : appointment.Date.Date.AddHours(16).AddMinutes(30);

            if (now > cutoff)
            {
                appointment.Status = AppointmentStatus.NoShow;
                return;
            }

            appointment.Status = AppointmentStatus.Confirmed;
            appointment.ConfirmedAt = now;
        }

        public async Task<Appointment?> GetByIdWithIncludes(int appointmentId)
        {
            return await _appointmentRepository.GetByIdWithIncludes(appointmentId);
        }
    }
}
