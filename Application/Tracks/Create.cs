using Domain;
using MediatR;
using Persistence;

namespace Application.Tracks
{
    public class Create
    {
        public class Command : IRequest
        {
            public Track Track { get; set; }
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
                _ctx.Tracks.Add(request.Track);
                await _ctx.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}