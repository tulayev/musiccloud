using Application.CQRS.Tracks.Queries;
using Application.DTOs.Tracks;
using Application.Helpers;
using Application.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.CQRS.Tracks.Handlers
{
    public class GetTracksQueryHandler : IRequestHandler<GetTracksQuery, ApiResponse<List<TrackDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTracksQueryHandler(IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<TrackDto>>> Handle(GetTracksQuery request, CancellationToken cancellationToken)
        {
            var tracks = await _unitOfWork.GetQueryable<Track>()
                .ProjectTo<TrackDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return ApiResponse<List<TrackDto>>.Success(tracks);
        }
    }
}
