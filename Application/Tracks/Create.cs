using Application.Core;
using Data;
using FluentValidation;
using MediatR;
using Models;

namespace Application.Tracks
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Track Track { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Track).SetValidator(new TrackValidator());
            }
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
                _ctx.Tracks.Add(request.Track);
                bool result = await _ctx.SaveChangesAsync() > 0;

                if (!result)
                    return Result<Unit>.Failure("Не удалось загрузить трек");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}