using Application.Core;
using Data;
using MediatR;

namespace Application.PlayLists
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _ctx;

            public Handler(DataContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var playList = await _ctx.PlayLists.FindAsync(request.Id);

                if (playList == null)
                    return null;

                _ctx.PlayLists.Remove(playList);
                
                await _ctx.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}