using Application.CQRS.Tracks.Commands;
using Application.Helpers;
using Application.Repository;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.CQRS.Tracks.Handlers
{
    public class UpdateTrackCommandHandler : IRequestHandler<UpdateTrackCommand, ApiResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTrackCommandHandler(IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Handle(UpdateTrackCommand request, CancellationToken cancellationToken)
        {
            var track = await _unitOfWork.GetQueryable<Track>()
                .FirstOrDefaultAsync(t => t.Id == request.UpdateTrackDto.Id, cancellationToken);

            if (track == null)
            {
                return ApiResponse<bool>.Failure(new Exception("Трек не найден"));
            }

            _mapper.Map(request.UpdateTrackDto, track);

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.Success(true);
        }
    }
}
