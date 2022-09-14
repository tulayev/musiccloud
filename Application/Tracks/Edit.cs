using Application.Core;
using Application.DTOs.Tracks;
using Application.Repository.IRepository;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Tracks
{
    public class Edit
    {
        public class Command : IRequest<Result<bool>> 
        {
            public EditTrackDTO Track { get; set; }
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
                var track = await _unitOfWork.GetQueryable<Track>()
                    .FirstOrDefaultAsync(t => t.Id == request.Track.Id);

                if (track == null)
                    return Result<bool>.Failure(new Exception("Трек не найден"));

                _mapper.Map(request.Track, track);
                
                await _unitOfWork.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
        }
    }
}