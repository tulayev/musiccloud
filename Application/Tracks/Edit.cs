using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Data;
using MediatR;
using Models;

namespace Application.Tracks
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>> 
        {
            public Track Track { get; set; }
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

                track.Title = request.Track.Title;
                track.Author = request.Track.Author;
                track.Genre = request.Track.Genre;
                
                await _ctx.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}