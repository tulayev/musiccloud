using Application.Core;
using Application.Repository.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.PlayLists
{
    public class Edit
    {
        public class Command : IRequest<Result<bool>> 
        {
            public PlayList PlayList { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var playList = await _unitOfWork.GetQueryable<PlayList>()
                    .FirstOrDefaultAsync(p => p.Id == request.PlayList.Id);

                if (playList == null) 
                    return null;

                playList.Name = request.PlayList.Name;

                await _unitOfWork.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
        }
    }
}