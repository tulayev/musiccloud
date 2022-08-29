using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            private readonly DataContext _ctx;
            
            private readonly IMapper _mapper;

            public Handler(DataContext ctx, IMapper mapper)
            {
                _ctx = ctx;
                _mapper = mapper;
            }

            public async Task<Result<TrackDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var track = await _ctx.Tracks
                    .ProjectTo<TrackDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(t => t.Id == request.Id);

                return Result<TrackDTO>.Success(track);
            }
        }
    }
}