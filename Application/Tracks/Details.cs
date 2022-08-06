using Domain;
using MediatR;
using Persistence;

namespace Application.Tracks
{
    public class Details
    {
        public class Query : IRequest<Track> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Track>
        {
            private readonly DataContext _ctx;

            public Handler(DataContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<Track> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _ctx.Tracks.FindAsync(request.Id);
            }
        }
    }
}