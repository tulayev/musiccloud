using Application.CQRS.PlayLists.Queries;
using Application.DTOs.PlayLists;
using Application.Helpers;
using Application.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.CQRS.PlayLists.Handlers
{
    public class GetPlayListByIdQueryHandler : IRequestHandler<GetPlayListByIdQuery, ApiResponse<PlayListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPlayListByIdQueryHandler(IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PlayListDto>> Handle(GetPlayListByIdQuery request, CancellationToken cancellationToken)
        {
            var playList = await _unitOfWork.GetQueryable<PlayList>()
                .ProjectTo<PlayListDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return ApiResponse<PlayListDto>.Success(playList);
        }
    }
}