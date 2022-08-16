using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Tracks
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>> 
        {
            public Track Track { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(t => t.Track).SetValidator(new TrackValidator());
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
                var track = await _ctx.Tracks.FindAsync(request.Track.Id);

                if (track == null) 
                    return null;

                _mapper.Map(request.Track, track);
                var result = await _ctx.SaveChangesAsync() > 0;

                if (!result)
                    return Result<Unit>.Failure("Не удалось изменить трек");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}