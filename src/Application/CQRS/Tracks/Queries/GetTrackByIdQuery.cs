using Application.DTOs.Tracks;
using Application.Helpers;
using MediatR;

namespace Application.CQRS.Tracks.Queries
{
    public record GetTrackByIdQuery(Guid Id) : IRequest<ApiResponse<TrackDto>>;
}
