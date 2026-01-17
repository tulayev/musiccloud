using Application.DTOs.Comments;
using Application.Helpers;
using MediatR;

namespace Application.CQRS.Comments.Queries
{
    public record GetCommentsQuery(Guid TrackId) : IRequest<ApiResponse<List<CommentDto>>>;
}
