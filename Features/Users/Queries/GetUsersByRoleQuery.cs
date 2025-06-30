using ClinicManagementSystem.DTOs.User;
using MediatR;

namespace ClinicManagementSystem.Features.Users.Queries
{
    public class GetUsersByRoleQuery : IRequest<List<UserSummaryDto>>
    {
        public string RoleName { get; set; }

        public GetUsersByRoleQuery(string roleName)
        {
            RoleName = roleName;
        }
    }

}
