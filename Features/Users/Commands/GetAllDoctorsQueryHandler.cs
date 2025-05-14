using AutoMapper;
using ClinicManagementSystem.DTOs.User;
using ClinicManagementSystem.Features.Users.Queries;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Users.Commands
{
    public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, List<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;   
        public GetAllDoctorsQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<List<UserDto>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
           var doctors =  await _userService.GetAllDoctors();
            return _mapper.Map<List<UserDto>>(doctors);
        }
    }
}
