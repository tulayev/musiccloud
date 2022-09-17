using Application.Core;
using Application.DTOs.PlayLists;
using Application.Repository.IRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.PlayLists
{
    public class Details
    {
        public class Query : IRequest<Result<PlayListDTO>> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PlayListDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;
            
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<PlayListDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var playList = await _unitOfWork.GetQueryable<PlayList>()
                    .ProjectTo<PlayListDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<PlayListDTO>.Success(playList);
            }
        }
    }
}