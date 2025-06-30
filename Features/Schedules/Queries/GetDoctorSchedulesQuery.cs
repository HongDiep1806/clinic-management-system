using ClinicManagementSystem.DTOs.Schedule;
using MediatR;

namespace ClinicManagementSystem.Features.Schedules.Queries
{
    public class GetDoctorSchedulesQuery : IRequest<List<ScheduleDto>>
    {
        public int DoctorId { get; set; }
        public GetDoctorSchedulesQuery(int doctorId)
        {
            DoctorId = doctorId;
        }
    }
}
