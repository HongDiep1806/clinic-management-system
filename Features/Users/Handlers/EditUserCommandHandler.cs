using AutoMapper;
using ClinicManagementSystem.DTOs.User;
using ClinicManagementSystem.Features.Users.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ClinicManagementSystem.Features.Users.Handlers
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, UserDto>
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;

        public EditUserCommandHandler(
            IUserService userService,
            IPasswordHasher<User> passwordHasher,
            IMapper mapper)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserById(request.UserId);
            if (user == null)
                throw new Exception("User not found");

            // mapping dto → existing user fields (password xử lý riêng)
            user.FullName = request.EditUserDto.FullName;
            user.Dob = request.EditUserDto.Dob;
            user.Gender = request.EditUserDto.Gender;
            user.Phone = request.EditUserDto.Phone;
            user.Address = request.EditUserDto.Address;
            user.DepartmentId = request.EditUserDto.DepartmentId;

            // password update
            if (!string.IsNullOrEmpty(request.EditUserDto.NewPassword))
            {
                user.HashPassword = _passwordHasher.HashPassword(user, request.EditUserDto.NewPassword);
            }

            // GỌI REPOSITORY (đã check email bên trong)
            var updatedUser = await _userService.EditUser(request.UserId, request.EditUserDto);

            return _mapper.Map<UserDto>(updatedUser);
        }

    }
}
