using Application.Common.Interfaces.Repository;
using Application.Common.Interfaces.Users;
using Application.CQRS.Comments.Commands;
using Application.DTOs.Comments;
using Application.Helpers;
using Application.Hubs;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.CQRS.Comments.Handlers
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ApiResponse<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessorService _userAccessorService;
        private readonly IHubContext<ChatHub> _hubContext;

        public CreateCommentCommandHandler(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IUserAccessorService userAccessorService,
            IHubContext<ChatHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessorService = userAccessorService;
            _hubContext = hubContext;
        }

        public async Task<ApiResponse<CommentDto>> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
        {
            var tracksCount = await _unitOfWork.GetQueryable<Track>()
                .Where(t => t.Id == command.TrackId)
                .CountAsync(cancellationToken);

            if (tracksCount == 0)
            {
                return null;
            }

            var comment = new Comment
            {
                AuthorId = _userAccessorService.User.Id,
                TrackId = command.TrackId,
                Body = command.Body
            };

            await _unitOfWork.AddAsync(comment);

            await _unitOfWork.SaveChangesAsync();

            await _hubContext.Clients.Group(comment.TrackId.ToString())
                .SendAsync("ReceiveComment", comment.Body, cancellationToken: cancellationToken);

            return ApiResponse<CommentDto>.Success(_mapper.Map<CommentDto>(comment));
        }
    }
}
