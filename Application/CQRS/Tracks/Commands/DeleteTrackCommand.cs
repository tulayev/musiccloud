using Application.Helpers;
using MediatR;

namespace Application.CQRS.Tracks.Commands
{
    public record DeleteTrackCommand(Guid Id) : IRequest<ApiResponse<Unit>>;
}
