using Application.DTOs.PlayLists;
using Application.Helpers;
using MediatR;

namespace Application.CQRS.PlayLists.Queries
{
    public record GetPlayListsQuery : IRequest<ApiResponse<List<PlayListDto>>>;
}
