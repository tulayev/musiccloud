using Application.Core;
using Application.DTOs;
using Application.Repository.IRepository;
using MediatR;

namespace Application.Tracks
{
    public class List
    {
        public class Query : IRequest<Result<List<TrackDTO>>> {}

        public class Handler : IRequestHandler<Query, Result<List<TrackDTO>>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<List<TrackDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<TrackDTO>>.Success(await _unitOfWork.TrackRepository.GetAll());
            }
        }
    }
}