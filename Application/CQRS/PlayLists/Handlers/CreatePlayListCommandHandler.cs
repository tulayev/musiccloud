using Application.Common.Interfaces.Repository;
using Application.Common.Interfaces.Users;
using Application.CQRS.PlayLists.Commands;
using Application.Helpers;
using MediatR;
using Models;

namespace Application.CQRS.PlayLists.Handlers
{
    public class CreatePlayListCommandHandler : IRequestHandler<CreatePlayListCommand, ApiResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessorService _userAccessorService;

        public CreatePlayListCommandHandler(IUnitOfWork unitOfWork, 
            IUserAccessorService userAccessorService)
        {
            _unitOfWork = unitOfWork;
            _userAccessorService = userAccessorService;
        }

        public async Task<ApiResponse<bool>> Handle(CreatePlayListCommand request, CancellationToken cancellationToken)
        {
            request.PlayList.UserId = _userAccessorService.User.Id;

            await _unitOfWork.AddAsync(request.PlayList);

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.Success(true);
        }
    }
}