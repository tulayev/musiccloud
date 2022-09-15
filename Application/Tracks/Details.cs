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
                var track = await _unitOfWork.GetQueryable<Track>()
                    .ProjectTo<TrackDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(dto => dto.Id == request.Id);

                return Result<TrackDTO>.Success(track);
            }
        }
    }
}