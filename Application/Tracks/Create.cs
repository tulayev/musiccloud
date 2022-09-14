using Application.Core;
using Application.DTOs.Tracks;
using Application.Interfaces;
using Application.Repository.IRepository;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Tracks
{
    public class Create
    {
        public class Command : IRequest<Result<bool>>
        {
            public CreateTrackDTO Track { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IUnitOfWork _unitOfWork;
            
            private readonly IUserAccessor _userAccessor;

            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                string username = _userAccessor.GetUsername();

                var user = await _unitOfWork.GetQueryable<User>()
                    .FirstOrDefaultAsync(u => u.UserName == username);

                request.Track.UserId = user.Id;

                await _unitOfWork.AddAsync(_mapper.Map<Track>(request.Track));
                await _unitOfWork.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
        }
    }
}