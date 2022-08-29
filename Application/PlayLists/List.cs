using Application.Core;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PlayLists
{
    public class List
    {
        public class Query : IRequest<Result<List<PlayListDTO>>> {}

        public class Handler : IRequestHandler<Query, Result<List<PlayListDTO>>>
        {
            private readonly DataContext _ctx;
            
            private readonly IUserAccessor _userAccessor;
            
            private readonly IMapper _mapper;

            public Handler(DataContext ctx, IUserAccessor userAccessor, IMapper mapper)
            {
                _ctx = ctx;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<Result<List<PlayListDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUsername());

                var userPlayLists = await _ctx.PlayLists
                        .ProjectTo<PlayListDTO>(_mapper.ConfigurationProvider)
                        .Where(p => p.Owner.Username == user.UserName)
                        .ToListAsync();

                return Result<List<PlayListDTO>>.Success(userPlayLists);
            }
        }
    }
}