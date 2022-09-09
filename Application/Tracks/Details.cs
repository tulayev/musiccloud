using Application.Core;
using Application.DTOs;
using Application.Repository.IRepository;
using AutoMapper;
using MediatR;

namespace Application.Tracks
{
    public class Details
    {
        public class Query : IRequest<Result<TrackDTO>> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TrackDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;
            
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<TrackDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var track = await _unitOfWork.TrackRepository.GetWithRelatedData(request.Id);

                return Result<TrackDTO>.Success(track);
            }
        }
    }
}