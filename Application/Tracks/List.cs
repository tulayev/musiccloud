using Application.Core;
using Application.DTOs.Tracks;
using Application.Repository.IRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                var tracks = await _unitOfWork.GetQueryable<Track>()
                    .ProjectTo<TrackDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                    
                return Result<List<TrackDTO>>.Success(tracks);
            }
        }
    }
}