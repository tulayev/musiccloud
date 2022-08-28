using Application.Core;
using MediatR;

namespace Application.Files
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string PublicId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            public Handler()
            {
                
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}