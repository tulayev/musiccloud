using Application.Core;
using Application.Interfaces;
using Application.Repository.IRepository;
using MediatR;
using Models;

namespace Application.Tracks
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Track Track { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUnitOfWork _unitOfWork;
            
            private readonly IUserAccessor _userAccessor;

            public Handler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _unitOfWork.UserRepository.GetAuthorizedUser(u => u.UserName == _userAccessor.GetUsername());

                request.Track.User = user;

                _unitOfWork.TrackRepository.Add(request.Track);
                await _unitOfWork.SaveChanges();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}