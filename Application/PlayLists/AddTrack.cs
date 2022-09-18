using Application.Core;
using Application.DTOs.PlayLists;
using Application.Repository.IRepository;
using AutoMapper;
using MediatR;
using Models;

namespace Application.PlayLists
{
    public class AddTrack
    {
        public class Command : IRequest<Result<bool>>
        {
            public AddTrackToPlayListDTO AddTrackToPlayListDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IUnitOfWork _unitOfWork;

            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;   
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                await _unitOfWork.AddAsync(_mapper.Map<PlayListTrack>(request.AddTrackToPlayListDTO));
                
                await _unitOfWork.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
        }
    }
}
