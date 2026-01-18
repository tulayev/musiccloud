using Application.DTOs.Tracks;
using Application.Helpers;
using MediatR;

namespace Application.CQRS.Tracks.Queries
{
    public record GetTracksQuery : IRequest<ApiResponse<List<TrackDto>>>;
}
