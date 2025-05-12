using AutoMapper;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Features.Appointments.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Handlers
{
    public class BookAppointmentCommandHandler : IRequestHandler<BookAppointmentCommand, BookAppointmentResponseDto>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public BookAppointmentCommandHandler(IMapper mapper, IAppointmentService appointmentService)
        {
            _mapper = mapper;
            _appointmentService = appointmentService;
        }

        public async Task<BookAppointmentResponseDto> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = new Appointment
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                AppointmentDate = request.AppointmentDate,
                Status = "Pending"
            };

            var newAppointment = await _appointmentService.CreateAppointment(appointment);
            return _mapper.Map<BookAppointmentResponseDto>(newAppointment);
        }
    }
}
