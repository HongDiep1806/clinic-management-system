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
                Date = dto.Date.Date,
                Reason = dto.Reason,
                Status = AppointmentStatus.Pending, // staff can also create pending
                CreatedAt = DateTime.Now
            };

            await ValidateAppointment(appointment);
            return await _appointmentRepository.Create(appointment);
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await _appointmentRepository.GetAllWithIncludes();
        }

        public async Task<Appointment> GetAppointmentById(int appointmentId)
        {
            return await _appointmentRepository.GetById(appointmentId);
        }

        public async Task<List<Appointment>> GetDoctorAppointments(int doctorId)
        {
            return await _appointmentRepository.GetDoctorAppointments(doctorId);
        }

        public async Task<List<Appointment>> GetPatientAppointments(int patientId)
        {
            return await _appointmentRepository.GetPatientAppointments(patientId);
        }

        public async Task<bool> UpdateAppointment(Appointment appointment, int appointmentId)
        {
            return await _appointmentRepository.Update(appointmentId, appointment);
        }

        private async Task ValidateAppointment(Appointment appointment)
        {
            var date = appointment.Date.Date;

            if (appointment.DoctorId == null)
                throw new InvalidOperationException("Doctor is required.");

            if (appointment.PatientId == null)
                throw new InvalidOperationException("Patient is required.");

            int doctorId = appointment.DoctorId.Value;
            int patientId = appointment.PatientId.Value;

            // 1. Doctor must work on this day
            var weekDay = (WeekDay)Enum.Parse(typeof(WeekDay), date.DayOfWeek.ToString());
            var schedule = await _scheduleRepository.GetDoctorScheduleAtDay(doctorId, weekDay);

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

    }
}
