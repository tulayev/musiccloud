using Application.Core;
using Application.Repository.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.PlayLists
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

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var playList = await _unitOfWork.GetQueryable<PlayList>()
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                if (playList == null)
                    return null;

                playList.Tracks?.Clear();

                _unitOfWork.Delete(playList);
                
                await _unitOfWork.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}