using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Commands
{
    public class CancelAppointmentCommand : IRequest<bool>
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }

        public CancelAppointmentCommand(int appointmentId, int patientId)
        {
            AppointmentId = appointmentId;
            PatientId = patientId;
        }
    }

}
