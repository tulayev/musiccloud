using Application.CQRS.Tracks.Commands;
using Application.Helpers;
using Application.Repository;
using Application.Services.Users;
using AutoMapper;
using MediatR;
using Models;

namespace Application.CQRS.Tracks.Handlers
{
    public class CreateTrackCommandHandler : IRequestHandler<CreateTrackCommand, ApiResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessorService _userAccessorService;
        private readonly IMapper _mapper;

        public CreateTrackCommandHandler(IUnitOfWork unitOfWork, 
            IUserAccessorService userAccessorService, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userAccessorService = userAccessorService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Handle(CreateTrackCommand request, CancellationToken cancellationToken)
        {
            request.CreateTrackDto.UserId = _userAccessorService.User.Id;

            await _unitOfWork.AddAsync(_mapper.Map<Track>(request.CreateTrackDto));
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.Success(true);
        }
    }
}
