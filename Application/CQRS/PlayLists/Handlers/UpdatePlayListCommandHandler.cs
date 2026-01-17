using Application.CQRS.PlayLists.Commands;
using Application.Helpers;
using Application.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.CQRS.PlayLists.Handlers
{
    public class UpdatePlayListCommandHandler : IRequestHandler<UpdatePlayListCommand, ApiResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePlayListCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<bool>> Handle(UpdatePlayListCommand request, CancellationToken cancellationToken)
        {
            var playList = await _unitOfWork.GetQueryable<PlayList>()
                .FirstOrDefaultAsync(p => p.Id == request.PlayList.Id, cancellationToken);

            if (playList == null)
            {
                return null;
            }

            playList.Name = request.PlayList.Name;

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.Success(true);
        }
    }
}
