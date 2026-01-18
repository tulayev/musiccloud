using Application.DTOs.Users;
using Application.Helpers;
using MediatR;

namespace Application.CQRS.Users.Queries
{
    public record GetCurrentUserQuery(string Email) : IRequest<ApiResponse<UserDto>>;
}
