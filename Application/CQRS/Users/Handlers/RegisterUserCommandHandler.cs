using Application.Common.Interfaces.Security;
using Application.CQRS.Users.Commands;
using Application.DTOs.Users;
using Application.Helpers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.CQRS.Users.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ApiResponse<UserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public RegisterUserCommandHandler(UserManager<User> userManager,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<UserDto>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            if (await _userManager.Users.AnyAsync(u => u.Email == command.RegisterDto.Email, cancellationToken: cancellationToken))
            {
                return ApiResponse<UserDto>.Failure(new Exception("Email уже зарегистрирован"));
            }

            if (await _userManager.Users.AnyAsync(u => u.UserName == command.RegisterDto.Username, cancellationToken: cancellationToken))
            {
                return ApiResponse<UserDto>.Failure(new Exception("UserName уже зарегистрирован"));
            }

            var user = new User
            {
                DisplayName = command.RegisterDto.DisplayName,
                Email = command.RegisterDto.Email,
                UserName = command.RegisterDto.Username
            };

            var result = await _userManager.CreateAsync(user, command.RegisterDto.Password);

            if (!result.Succeeded)
            {
                return ApiResponse<UserDto>.Failure(new Exception("Возникла ошибка при регистрации!"));
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);

            return ApiResponse<UserDto>.Success(userDto);
        }
    }
}
