using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Tracks
{
    public class Edit
    {
        public class Command : IRequest 
        {
            public Track Track { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _ctx;
            private readonly IMapper _mapper;

            public Handler(DataContext ctx, IMapper mapper)
            {
                _ctx = ctx;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var track = await _ctx.Tracks.FindAsync(request.Track.Id);
                _mapper.Map(request.Track, track);
                await _ctx.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}