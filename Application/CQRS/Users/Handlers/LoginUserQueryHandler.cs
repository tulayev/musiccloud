using Application.Common.Interfaces.Security;
using Application.CQRS.Users.Queries;
using Application.DTOs.Users;
using Application.Helpers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Application.CQRS.Users.Handlers
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ApiResponse<UserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public LoginUserQueryHandler(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<UserDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);

            if (user == null)
            {
                return ApiResponse<UserDto>.Failure(new Exception("Пользователь не найден"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password, false);

            if (!result.Succeeded)
            {
                return ApiResponse<UserDto>.Failure(new Exception("Ошибка логина"));
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);

            return ApiResponse<UserDto>.Success(userDto);
        }
    }
}
