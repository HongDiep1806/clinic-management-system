using AutoMapper;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.DTOs.User;
using ClinicManagementSystem.Features.Users.Queries;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Users.Handlers
{
    public class GetUsersByRoleQueryHandler : IRequestHandler<GetUsersByRoleQuery, List<UserSummaryDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUsersByRoleQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService; 
            _mapper = mapper;
        }

        public async Task<List<UserSummaryDto>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetUsersByRole(request.RoleName);    

            return _mapper.Map<List<UserSummaryDto>>(users);
        }
    }

}
