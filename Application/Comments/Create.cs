using Application.Core;
using Application.DTOs;
using Application.Interfaces;
using Application.Repository.IRepository;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Comments
{
    public class Create
    {
        public class Command : IRequest<Result<CommentDTO>>
        {
            public Guid TrackId { get; set; }

            public string Body { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<CommentDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;
            
            private readonly IMapper _mapper;
            
            private readonly IUserAccessor _userAccessor;
            
            public Handler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<CommentDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var track = await _unitOfWork.GetQueryable<Track>()
                    .FirstOrDefaultAsync(t => t.Id == request.TrackId);

                if (track == null)
                    return null;

                var user = await _unitOfWork.GetQueryable<User>()
                    .FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUsername());

                var comment = new Comment
                {
                    Author = user,
                    Track = track,
                    Body = request.Body
                };

                await _unitOfWork.AddAsync(comment);
                await _unitOfWork.SaveChangesAsync();

                return Result<CommentDTO>.Success(_mapper.Map<CommentDTO>(comment));
            }
        }
    }
}