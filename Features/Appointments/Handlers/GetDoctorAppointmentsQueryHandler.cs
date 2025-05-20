using AutoMapper;
using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Features.Appointments.Queries;
using ClinicManagementSystem.Repositories;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Handlers
{
    public class GetDoctorAppointmentsQueryHandler: IRequestHandler<GetDoctorAppointmentsQuery, List<DoctorAppointmentResponseDto>>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetDoctorAppointmentsQueryHandler(IAppointmentService appointmentService, IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _appointmentService = appointmentService;
            _userService = userService;
        }
        public async Task<List<DoctorAppointmentResponseDto>> Handle(GetDoctorAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var doctorExists = await _userService.DoesDoctorExits(request.RequestDto.DoctorId);
            if (!doctorExists)
                throw new ArgumentException("Doctor not found");

            var appointments = await _appointmentService.GetDoctorAppointments(request.RequestDto.DoctorId, request.RequestDto.Date);
            var doctorAppointments = _mapper.Map<List<DoctorAppointmentResponseDto>>(appointments);

            return doctorAppointments;
        }
    }

}
