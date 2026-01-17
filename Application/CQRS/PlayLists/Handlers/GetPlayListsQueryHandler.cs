using Application.CQRS.PlayLists.Queries;
using Application.DTOs.PlayLists;
using Application.Helpers;
using Application.Repository;
using Application.Services.Users;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.CQRS.PlayLists.Handlers
{
    public class GetPlayListsQueryHandler : IRequestHandler<GetPlayListsQuery, ApiResponse<List<PlayListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessorService _userAccessorService;
        private readonly IMapper _mapper;

        public GetPlayListsQueryHandler(IUnitOfWork unitOfWork, 
            IUserAccessorService userAccessorService, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userAccessorService = userAccessorService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<PlayListDto>>> Handle(GetPlayListsQuery request, CancellationToken cancellationToken)
        {
            var userPlayLists = await _unitOfWork.GetQueryable<PlayList>()
                .ProjectTo<PlayListDto>(_mapper.ConfigurationProvider)
                .Where(p => p.Owner.Username == _userAccessorService.User.UserName)
                .ToListAsync(cancellationToken);

            return ApiResponse<List<PlayListDto>>.Success(userPlayLists);
        }
    }
}