using Application.CQRS.Comments.Queries;
using Application.DTOs.Comments;
using Application.Helpers;
using Application.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.CQRS.Comments.Handlers
{
    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, ApiResponse<List<CommentDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCommentsQueryHandler(IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CommentDto>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _unitOfWork.GetQueryable<Comment>()
                .Where(c => c.Track.Id == request.TrackId)
                .OrderBy(c => c.CreatedAtUtc)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return ApiResponse<List<CommentDto>>.Success(comments);
        }
    }
}
