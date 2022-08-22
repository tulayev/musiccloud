using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.PlayLists
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public PlayList PlayList { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.PlayList).SetValidator(new PlayListValidator());
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
                _ctx.PlayLists.Add(request.PlayList);
                bool result = await _ctx.SaveChangesAsync() > 0;

                if (!result)
                    return Result<Unit>.Failure("Не удалось создать пдейлист");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}