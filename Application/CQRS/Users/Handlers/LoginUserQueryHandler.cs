using Application.CQRS.Users.Queries;
using Application.DTOs.Users;
using Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Application.CQRS.Users.Handlers
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ApiResponse<UserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginUserQueryHandler(UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            
        }

        public async Task<ApiResponse<UserDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);

            if (user == null)
            {
                return ApiResponse<UserDto>.Failure(new Exception("Пользователь не найден"));
            }
        }
    }
}
