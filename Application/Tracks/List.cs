using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tracks
{
    public class List
    {
        public class Query : IRequest<Result<List<Track>>> {}

        public class Handler : IRequestHandler<Query, Result<List<Track>>>
        {
            private readonly DataContext _ctx;

            public Handler(DataContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<Result<List<Track>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<Track>>.Success(await _ctx.Tracks.ToListAsync());
            }
        }

    }
}