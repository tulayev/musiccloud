using Application.Common.Interfaces.Repository;
using Application.CQRS.Comments.Queries;
using Application.DTOs.Comments;
using Application.Helpers;
using Application.Hubs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.CQRS.Comments.Handlers
{
    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, ApiResponse<List<CommentDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public GetCommentsQueryHandler(IUnitOfWork unitOfWork, 
            IMapper mapper,
            IHubContext<ChatHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<ApiResponse<List<CommentDto>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _unitOfWork.GetQueryable<Comment>()
                .Where(c => c.Track.Id == request.TrackId)
                .OrderBy(c => c.CreatedAtUtc)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            await _hubContext.Clients
                .Group(request.TrackId.ToString())
                .SendAsync("LoadComments", comments, cancellationToken: cancellationToken);

            return ApiResponse<List<CommentDto>>.Success(comments);
        }
    }
}
