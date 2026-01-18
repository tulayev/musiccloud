using Application.DTOs.PlayLists;
using Application.Helpers;
using MediatR;

namespace Application.CQRS.PlayLists.Queries
{
    public record GetPlayListByIdQuery(Guid Id) : IRequest<ApiResponse<PlayListDto>>;
}
