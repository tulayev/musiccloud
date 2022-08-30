using Application.Core;
using Application.Interfaces;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Tracks
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Track Track { get; set; }
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
                var poster = await _ctx.Files.FirstOrDefaultAsync(f => f.Id == request.Track.Poster.Id);

                request.Track.User = user;
                request.Track.Poster = poster;

                _ctx.Tracks.Add(request.Track);
                bool result = await _ctx.SaveChangesAsync() > 0;

                if (!result)
                    return Result<Unit>.Failure("Не удалось загрузить трек");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}