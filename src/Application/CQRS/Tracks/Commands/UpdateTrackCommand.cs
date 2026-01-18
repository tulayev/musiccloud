using Application.DTOs.Tracks;
using Application.Helpers;
using MediatR;

namespace Application.CQRS.Tracks.Commands
{
    public record UpdateTrackCommand(UpdateTrackDto UpdateTrackDto) : IRequest<ApiResponse<bool>>;
}
