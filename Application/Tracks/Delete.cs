using Application.Core;
using Data;
using MediatR;

namespace Application.Tracks
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
                var track = await _ctx.Tracks.FindAsync(request.Id);

                if (track == null)
                    return null;

                _ctx.Tracks.Remove(track);
                bool result = await _ctx.SaveChangesAsync() > 0;

                if (!result)
                    return Result<Unit>.Failure("Не удалось удалить трек");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}