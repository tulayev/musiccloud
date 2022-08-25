using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tracks
{
    public class List
    {
        public class Query : IRequest<Result<List<TrackDTO>>> {}

        public class Handler : IRequestHandler<Query, Result<List<TrackDTO>>>
        {
            private readonly DataContext _ctx;
            
            private readonly IMapper _mapper;

            public Handler(DataContext ctx, IMapper mapper)
            {
                _ctx = ctx;
                _mapper = mapper;
            }

            public async Task<Result<List<TrackDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tracks = await _ctx.Tracks
                        .ProjectTo<TrackDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync();

                return Result<List<TrackDTO>>.Success(tracks);
            }
        }
    }
}