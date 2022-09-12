using Application.Core;
using Application.DTOs;
using Application.Repository.IRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Models;

namespace Application.Tracks
{
    public class List
    {
        public class Query : IRequest<Result<List<TrackDTO>>> {}

        public class Handler : IRequestHandler<Query, Result<List<TrackDTO>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<List<TrackDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tracks = _unitOfWork.GetQueryable<Track>()
                    .ProjectTo<TrackDTO>(_mapper.ConfigurationProvider)
                    .ToList();
                return Result<List<TrackDTO>>.Success(tracks);
            }
        }
    }
}