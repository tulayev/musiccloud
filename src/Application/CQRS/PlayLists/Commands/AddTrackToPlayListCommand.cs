using Application.DTOs.PlayLists;
using Application.Helpers;
using MediatR;

namespace Application.CQRS.PlayLists.Commands
{
    public record AddTrackToPlayListCommand(AddTrackToPlayListDto AddTrackToPlayListDTO) : IRequest<ApiResponse<bool>>;
}
