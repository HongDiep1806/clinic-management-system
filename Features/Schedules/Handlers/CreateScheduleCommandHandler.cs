using AutoMapper;
using ClinicManagementSystem.DTOs.Schedule;
using ClinicManagementSystem.Features.Schedules.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Features.Schedules.Handlers
{
    public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, ScheduleDto>
    {
        private readonly IMapper _mapper;
        private readonly IScheduleService _scheduleService;
        public CreateScheduleCommandHandler(IMapper mapper, IScheduleService scheduleService)
        {
            _mapper = mapper;
            _scheduleService = scheduleService;
        }
        public async Task<ScheduleDto> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = _mapper.Map<Schedule>(request.RequestDto);
            return _mapper.Map<ScheduleDto>(await _scheduleService.CreateSchedule(schedule));
        }

    }

}
