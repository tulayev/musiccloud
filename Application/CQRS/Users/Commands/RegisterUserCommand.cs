using Application.DTOs.Users;
using Application.Helpers;
using MediatR;

namespace Application.CQRS.Users.Commands
{
    public record RegisterUserCommand(RegisterDto RegisterDto) : IRequest<ApiResponse<UserDto>>;
}
