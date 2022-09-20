using Application.Core;
using Application.Repository.IRepository;
using Application.Services;
using MediatR;
using Models;

namespace Application.PlayLists
{
    public class Create
    {
        public class Command : IRequest<Result<bool>>
        {
            public PlayList PlayList { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IUnitOfWork _unitOfWork;
            
            private readonly IUserAccessor _userAccessor;

            public Handler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                request.PlayList.UserId = _userAccessor.User.Id;

                await _unitOfWork.AddAsync(request.PlayList);
                
                await _unitOfWork.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
        }
    }
}