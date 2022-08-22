using Application.Core;
using Data;
using MediatR;
using Models;

namespace Application.PlayLists
{
    public class Details
    {
        public class Query : IRequest<Result<PlayList>> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PlayList>>
        {
            private readonly DataContext _ctx;

            public Handler(DataContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<Result<PlayList>> Handle(Query request, CancellationToken cancellationToken)
            {
                var playList = await _ctx.PlayLists.FindAsync(request.Id);

                return Result<PlayList>.Success(playList);
            }
        }
    }
}