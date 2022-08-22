using Application.Core;
using Data;
using MediatR;
using Models;

namespace Application.Tracks
{
    public class Details
    {
        public class Query : IRequest<Result<Track>> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Track>>
        {
            private readonly DataContext _ctx;

            public Handler(DataContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<Result<Track>> Handle(Query request, CancellationToken cancellationToken)
            {
                var track = await _ctx.Tracks.FindAsync(request.Id);

                return Result<Track>.Success(track);
            }
        }
    }
}