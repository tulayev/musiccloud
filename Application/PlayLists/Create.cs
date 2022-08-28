using Application.Core;
using Application.Interfaces;
using Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

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
                RuleFor(c => c.PlayList).SetValidator(new PlayListValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _ctx;
            
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IUserAccessor userAccessor)
            {
                _ctx = ctx;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUsername());

                request.PlayList.User = user;

                _ctx.PlayLists.Add(request.PlayList);
                bool result = await _ctx.SaveChangesAsync() > 0;

                if (!result)
                    return Result<Unit>.Failure("Не удалось создать пдейлист");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}