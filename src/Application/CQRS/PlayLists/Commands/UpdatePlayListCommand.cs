using Application.Helpers;
using MediatR;
using Models;

namespace Application.CQRS.PlayLists.Commands
{
    public record UpdatePlayListCommand(PlayList PlayList) : IRequest<ApiResponse<bool>>;
}
