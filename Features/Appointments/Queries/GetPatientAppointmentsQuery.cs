using ClinicManagementSystem.DTOs.Appointment;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Queries
{
    public class GetPatientAppointmentsQuery:IRequest<List<PatientAppointmentResponseDto>>
    {
        public PatientAppointmentsRequestDto RequestDto { get; set; }
        public GetPatientAppointmentsQuery(PatientAppointmentsRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }
}
