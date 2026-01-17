using Application.DTOs.Tracks;
using Application.Helpers;
using MediatR;

namespace Application.CQRS.Tracks.Commands
{
    public record CreateTrackCommand(CreateTrackDto CreateTrackDto) : IRequest<ApiResponse<bool>>;
}
