using ClinicManagementSystem.DTOs.Appointment;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Queries
{
    public class GetDoctorAppointmentsQuery: IRequest<List<DoctorAppointmentResponseDto>>
    {
        public DoctorAppointmentsRequestDto RequestDto { get; set; }
        public GetDoctorAppointmentsQuery(DoctorAppointmentsRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }
}
