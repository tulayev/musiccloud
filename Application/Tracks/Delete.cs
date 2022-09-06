using Application.Core;
using Application.Interfaces;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            
            private readonly IFileAccessor _fileAccessor;

            public Handler(DataContext ctx, IFileAccessor fileAccessor)
            {
                _ctx = ctx;
                _fileAccessor = fileAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var track = await _ctx.Tracks
                    .Include(t => t.Audio)
                    .Include(t => t.Poster)
                    .FirstOrDefaultAsync(t => t.Id == request.Id);

                if (track == null)
                    return null;

                if (track.Poster != null) 
                {
                    await _fileAccessor.DeleteFile(track.Poster.PublicId);
                    _ctx.Files.Remove(track.Poster);
                }

                if (track.Audio != null)
                {
                    await _fileAccessor.DeleteFile(track.Audio.PublicId);
                    _ctx.Files.Remove(track.Audio);
                }

                _ctx.Tracks.Remove(track);
                bool result = await _ctx.SaveChangesAsync() > 0;

                if (!result)
                    return Result<Unit>.Failure("Не удалось удалить трек");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}