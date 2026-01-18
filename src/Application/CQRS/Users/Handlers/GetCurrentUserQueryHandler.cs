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
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ApiResponse<UserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public GetCurrentUserQueryHandler(UserManager<User> userManager,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);

            return ApiResponse<UserDto>.Success(userDto);
        }
    }
}
