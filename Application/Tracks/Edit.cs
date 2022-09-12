using Application.Core;
using Application.DTOs;
using Application.Repository.IRepository;
using AutoMapper;
using MediatR;
using Models;

namespace Application.Tracks
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>> 
        {
            public TrackDTO Track { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUnitOfWork _unitOfWork;
            
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var track = _unitOfWork.GetQueryable<Track>()
                    .FirstOrDefault(t => t.Id == request.Track.Id);

                _mapper.Map(request.Track, track);
                
                await _unitOfWork.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}