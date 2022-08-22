using Application.Core;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.PlayLists
{
    public class List
    {
        public class Query : IRequest<Result<List<PlayList>>> {}

        public class Handler : IRequestHandler<Query, Result<List<PlayList>>>
        {
            private readonly DataContext _ctx;

            public Handler(DataContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<Result<List<PlayList>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<PlayList>>.Success(await _ctx.PlayLists.ToListAsync());
            }
        }
    }
}