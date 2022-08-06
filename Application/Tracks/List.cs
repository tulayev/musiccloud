using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tracks
{
    public class List
    {
        public class Query : IRequest<List<Track>> {}

        public class Handler : IRequestHandler<Query, List<Track>>
        {
            private readonly DataContext _ctx;

            public Handler(DataContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<List<Track>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _ctx.Tracks.ToListAsync();
            }
        }

    }
}