using Application.Common.Interfaces.Repository;
using Application.CQRS.PlayLists.Commands;
using Application.Helpers;
using AutoMapper;
using MediatR;
using Models;

namespace Application.CQRS.PlayLists.Handlers
{
    public class AddTrackToPlayListCommandHandler : IRequestHandler<AddTrackToPlayListCommand, ApiResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddTrackToPlayListCommandHandler(IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Handle(AddTrackToPlayListCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.AddAsync(_mapper.Map<PlayListTrack>(request.AddTrackToPlayListDTO));

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.Success(true);
        }
    }
}
