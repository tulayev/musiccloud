using Application.Core;
using Application.DTOs.PlayLists;
using Application.Interfaces;
using Application.Repository.IRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.PlayLists
{
    public class List
    {
        public class Query : IRequest<Result<List<PlayListDTO>>> {}

        public class Handler : IRequestHandler<Query, Result<List<PlayListDTO>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            
            private readonly IUserAccessor _userAccessor;
            
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<Result<List<PlayListDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string username = _userAccessor.GetUsername();  

                var user = await _unitOfWork.GetQueryable<User>()
                    .FirstOrDefaultAsync(u => u.UserName == username);

                var userPlayLists = await _unitOfWork.GetQueryable<PlayList>()
                        .ProjectTo<PlayListDTO>(_mapper.ConfigurationProvider)
                        .Where(p => p.Owner.Username == user.UserName)
                        .ToListAsync();

                return Result<List<PlayListDTO>>.Success(userPlayLists);
            }
        }
    }
}