using AutoMapper;
using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Features.Appointments.Queries;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Handlers
{
    public class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, List<AppointmentDto>>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;
        public GetAllAppointmentsQueryHandler(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        public async Task<List<AppointmentDto>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentService.GetAllAppointments();
            return _mapper.Map<List<AppointmentDto>>(appointments);
        }
    }
}
