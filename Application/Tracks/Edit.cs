using Application.Core;
using Application.Infrastructure;
using AutoMapper;
using Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

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
                RuleFor(c => c.Track).SetValidator(new TrackValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _ctx;
            
            private readonly IMapper _mapper;
            
            private readonly IUserAccessor _userAccessor;

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

                track.Title = request.Track.Title;
                track.Author = request.Track.Author;
                track.Genre = request.Track.Genre;
                
                bool result = await _ctx.SaveChangesAsync() > 0;

                if (!result)
                    return Result<Unit>.Failure("Не удалось изменить трек");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}