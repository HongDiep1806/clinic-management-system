using ClinicManagementSystem.DTOs.User;
using MediatR;

namespace ClinicManagementSystem.Features.Users.Commands
{
    public class EditUserCommand : IRequest<UserDto>
    {
        public int UserId { get; set; }
        public EditUserDto EditUserDto { get; set; }
    }
}
