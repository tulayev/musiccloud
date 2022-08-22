using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PlayLists
{
    public class Details
    {
        public class Query : IRequest<Result<PlayListDTO>> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PlayListDTO>>
        {
            private readonly DataContext _ctx;
            
            private readonly IMapper _mapper;

            public Handler(DataContext ctx, IMapper mapper)
            {
                _ctx = ctx;
                _mapper = mapper;
            }

            public async Task<Result<PlayListDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var playList = await _ctx.PlayLists
                    .ProjectTo<PlayListDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<PlayListDTO>.Success(playList);
            }
        }
    }
}