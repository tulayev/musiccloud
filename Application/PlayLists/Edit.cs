using Application.Core;
using AutoMapper;
using Data;
using MediatR;
using Models;

namespace Application.PlayLists
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>> 
        {
            public PlayList PlayList { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _ctx;
            private readonly IMapper _mapper;

            public Handler(DataContext ctx, IMapper mapper)
            {
                _ctx = ctx;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var playList = await _ctx.PlayLists.FindAsync(request.PlayList.Id);

                if (playList == null) 
                    return null;

                playList.Name = request.PlayList.Name;

                await _ctx.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}