using AutoMapper;
using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Features.Appointments.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Handlers
{
    public class CreateAppointmentByStaffHandler : IRequestHandler<CreateAppointmentByStaffCommand, AppointmentDto>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public CreateAppointmentByStaffHandler(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> Handle(CreateAppointmentByStaffCommand request, CancellationToken cancellationToken)
        {
            var appointment =  await _appointmentService.CreateAppointmentByStaff(request.Dto);
            return _mapper.Map<AppointmentDto>(appointment);    
        }
    }
}
