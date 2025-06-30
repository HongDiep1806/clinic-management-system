using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IScheduleRepository _scheduleRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository, IScheduleRepository scheduleRepository)
        {
            _appointmentRepository = appointmentRepository;
            _scheduleRepository = scheduleRepository;
        }
        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            await ValidateAppointment(appointment);
            return await _appointmentRepository.Create(appointment);
        }

        public async Task<Appointment> CreateAppointmentByStaff(CreateAppointmentByStaffDto dto)
        {
            var appointmentDateTime = dto.Date.Date + dto.Time;

            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = appointmentDateTime,
                Status = AppointmentStatus.Confirmed,
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

        public async Task<bool> IsPatientAppointmentConflict(int patientId, DateTime dateTime)
        {
           return await _appointmentRepository.IsPatientAppointmentConflict(patientId, dateTime);
        }

        public async Task<bool> UpdateAppointment(Appointment appointment, int appointmentId)
        {
            return await _appointmentRepository.Update(appointmentId, appointment);
        }
        private async Task ValidateAppointment(Appointment appointment)
        {
            var appointmentTime = appointment.AppointmentDate;
            var weekDay = (WeekDay)Enum.Parse(typeof(WeekDay), appointmentTime.DayOfWeek.ToString());

            // Check bác sĩ có trực không
            var schedule = await _scheduleRepository.GetDoctorScheduleAtTime(
                appointment.DoctorId, weekDay, appointmentTime.TimeOfDay);

            if (schedule == null)
                throw new InvalidOperationException("Doctor is not scheduled to work at this time.");

            // Check bệnh nhân không trùng lịch
            if (await _appointmentRepository.IsPatientAppointmentConflict(appointment.PatientId, appointmentTime))
                throw new InvalidOperationException("Patient already has an appointment at this time.");

            // Check bác sĩ không bị double-book
            if (await _appointmentRepository.IsDoctorAppointmentConflict(appointment.DoctorId, appointmentTime))
                throw new InvalidOperationException("Doctor already has an appointment at this time.");
        }



    }
}
