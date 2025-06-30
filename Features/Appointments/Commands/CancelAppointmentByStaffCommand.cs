using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Commands
{
    public class CancelAppointmentByStaffCommand : IRequest<bool>
    {
        public int AppointmentId { get; set; }

        public CancelAppointmentByStaffCommand(int appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }
}
