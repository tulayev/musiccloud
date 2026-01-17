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
    public class GetTrackByIdQueryHandler : IRequestHandler<GetTrackByIdQuery, ApiResponse<TrackDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTrackByIdQueryHandler(IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<TrackDto>> Handle(GetTrackByIdQuery request, CancellationToken cancellationToken)
        {
            var track = await _unitOfWork.GetQueryable<Track>()
                .ProjectTo<TrackDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(dto => dto.Id == request.Id, cancellationToken);

            return ApiResponse<TrackDto>.Success(track);
        }
    }
}
