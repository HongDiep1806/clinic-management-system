using ClinicManagementSystem.DTOs.User;
using MediatR;

namespace ClinicManagementSystem.Features.Users.Queries
{
    public class GetAllPatientsQuery: IRequest<List<UserDto>>
    {
        public GetAllPatientsQuery()
        {

        }
    }
    
}
