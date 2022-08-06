using MediatR;
using Persistence;

namespace Application.Tracks
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _ctx;

            public Handler(DataContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var track = await _ctx.Tracks.FindAsync(request.Id);
                _ctx.Tracks.Remove(track);
                await _ctx.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}