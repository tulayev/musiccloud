using Application.CQRS.PlayLists.Commands;
using Application.Helpers;
using Application.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.CQRS.PlayLists.Handlers
{
    public class DeletePlayListCommandHandler : IRequestHandler<DeletePlayListCommand, ApiResponse<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePlayListCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<Unit>> Handle(DeletePlayListCommand request, CancellationToken cancellationToken)
        {
            var playList = await _unitOfWork.GetQueryable<PlayList>()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (playList == null)
            {
                return null;
            }

            playList.Tracks?.Clear();

            _unitOfWork.Delete(playList);

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<Unit>.Success(Unit.Value);
        }
    }
}
