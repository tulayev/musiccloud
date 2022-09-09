using Application.Core;
using Application.Interfaces;
using Application.Repository.IRepository;
using MediatR;

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
            private readonly IUnitOfWork _unitOfWork;
            
            private readonly IFileAccessor _fileAccessor;

            public Handler(IUnitOfWork unitOfWork, IFileAccessor fileAccessor)
            {
                _unitOfWork = unitOfWork;
                _fileAccessor = fileAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var track = await _unitOfWork.TrackRepository
                    .Get(predicate: t => t.Id == request.Id, includes: "Poster, Audio");

                if (track == null)
                    return null;

                if (track.Poster != null) 
                {
                    await _fileAccessor.DeleteFile(track.Poster.PublicId);
                    _unitOfWork.FileRepository.Delete(track.Poster);
                }

                if (track.Audio != null)
                {
                    await _fileAccessor.DeleteFile(track.Audio.PublicId);
                    _unitOfWork.FileRepository.Delete(track.Audio);
                }

                _unitOfWork.TrackRepository.Delete(track);
                await _unitOfWork.SaveChanges();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}