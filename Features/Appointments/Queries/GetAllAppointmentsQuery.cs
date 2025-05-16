using ClinicManagementSystem.DTOs.Appointment;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Queries
{
    public class GetAllAppointmentsQuery: IRequest<List<AppointmentDto>>
    {
    }
}
