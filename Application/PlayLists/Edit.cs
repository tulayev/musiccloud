using Application.Core;
using AutoMapper;
using Data;
using FluentValidation;
using MediatR;
using Models;

namespace Application.PlayLists
{
    public class Edit
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
            private readonly IMapper _mapper;

            public Handler(DataContext ctx, IMapper mapper)
            {
                _ctx = ctx;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var playList = await _ctx.PlayLists.FindAsync(request.PlayList.Id);

                if (playList == null) 
                    return null;

                _mapper.Map(request.PlayList, playList);
                bool result = await _ctx.SaveChangesAsync() > 0;

                if (!result)
                    return Result<Unit>.Failure("Не удалось изменить плейлист");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}