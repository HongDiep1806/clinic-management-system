using AutoMapper;
using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Features.Appointments.Queries;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Handlers
{
   
    
    public class GetPatientAppointmentsQueryHandler : IRequestHandler<GetPatientAppointmentsQuery, List<PatientAppointmentResponseDto>>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetPatientAppointmentsQueryHandler(IAppointmentService appointmentService, IUserService userService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<List<PatientAppointmentResponseDto>> Handle(GetPatientAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var doctorExists = await _userService.DoesPatientExists(request.RequestDto.PatientId);
            if (!doctorExists)
                throw new ArgumentException("Patient not found");

            var appointments = await _appointmentService.GetPatientAppointments(request.RequestDto.PatientId, request.RequestDto.Date);
            var patientAppointments = _mapper.Map<List<PatientAppointmentResponseDto>>(appointments);

            return patientAppointments;
        }
    }
}
