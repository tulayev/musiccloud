using Application.Helpers;
using MediatR;
using Models;

namespace Application.CQRS.PlayLists.Commands
{
    public record CreatePlayListCommand(PlayList PlayList) : IRequest<ApiResponse<bool>>;
}
