using Application.CQRS.Comments.Commands;
using Application.DTOs.Comments;
using Application.Helpers;
using Application.Repository;
using Application.Services.Users;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.CQRS.Comments.Handlers
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ApiResponse<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessorService _userAccessorService;

        public CreateCommentCommandHandler(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IUserAccessorService userAccessorService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessorService = userAccessorService;
        }

        public async Task<ApiResponse<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var tracksCount = await _unitOfWork.GetQueryable<Track>()
                .Where(t => t.Id == request.TrackId)
                .CountAsync(cancellationToken);

            if (tracksCount == 0)
            {
                return null;
            }

            var comment = new Comment
            {
                AuthorId = _userAccessorService.User.Id,
                TrackId = request.TrackId,
                Body = request.Body
            };

            await _unitOfWork.AddAsync(comment);

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<CommentDto>.Success(_mapper.Map<CommentDto>(comment));
        }
    }
}
