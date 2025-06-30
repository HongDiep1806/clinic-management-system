using ClinicManagementSystem.DTOs.Schedule;
using MediatR;

namespace ClinicManagementSystem.Features.Schedules.Commands
{
    public class CreateScheduleCommand : IRequest<ScheduleDto>
    {
        public CreateScheduleDto RequestDto { get; set; }

        public CreateScheduleCommand(CreateScheduleDto dto)
        {
            RequestDto = dto;
        }
    }

}
