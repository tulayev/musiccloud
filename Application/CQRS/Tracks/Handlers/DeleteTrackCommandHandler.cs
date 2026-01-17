using Application.CQRS.Tracks.Commands;
using Application.Helpers;
using Application.Repository;
using Application.Services.Files;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.CQRS.Tracks.Handlers
{
    public class DeleteTrackCommandHandler : IRequestHandler<DeleteTrackCommand, ApiResponse<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileAccessorService _fileAccessorService;

        public DeleteTrackCommandHandler(IUnitOfWork unitOfWork, 
            IFileAccessorService fileAccessorService)
        {
            _unitOfWork = unitOfWork;
            _fileAccessorService = fileAccessorService;
        }

        public async Task<ApiResponse<Unit>> Handle(DeleteTrackCommand request, CancellationToken cancellationToken)
        {
            var track = await _unitOfWork.GetQueryable<Track>()
                .Include(t => t.Poster)
                .Include(t => t.Audio)
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (track == null)
            {
                return null;
            }

            if (track.Poster != null)
            {
                await _fileAccessorService.DeleteFile(track.Poster.PublicId);
                _unitOfWork.Delete(track.Poster);
            }

            if (track.Audio != null)
            {
                await _fileAccessorService.DeleteFile(track.Audio.PublicId);
                _unitOfWork.Delete(track.Audio);
            }

            _unitOfWork.Delete(track);

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<Unit>.Success(Unit.Value);
        }
    }
}
