using Application.DTOs.Comments;
using Application.Helpers;
using MediatR;

namespace Application.CQRS.Comments.Commands
{
    public record CreateCommentCommand(Guid TrackId, string Body) : IRequest<ApiResponse<CommentDto>>;
}
