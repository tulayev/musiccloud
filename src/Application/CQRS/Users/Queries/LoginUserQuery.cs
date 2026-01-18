using Application.DTOs.Users;
using Application.Helpers;
using MediatR;

namespace Application.CQRS.Users.Queries
{
    public record LoginUserQuery(LoginDto LoginDto) : IRequest<ApiResponse<UserDto>>;
}
