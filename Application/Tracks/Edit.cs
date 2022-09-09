using Application.Core;
using Application.DTOs;
using Application.Repository.IRepository;
using MediatR;

namespace Application.Tracks
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>> 
        {
            public TrackDTO Track { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                await _unitOfWork.TrackRepository.Update(request.Track);
                
                await _unitOfWork.SaveChanges();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}