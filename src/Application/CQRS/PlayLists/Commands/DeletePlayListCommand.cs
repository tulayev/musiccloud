using Application.Helpers;
using MediatR;

namespace Application.CQRS.PlayLists.Commands
{
    public record DeletePlayListCommand(Guid Id) : IRequest<ApiResponse<Unit>>;
}
