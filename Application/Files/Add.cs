using Application.Core;
using Application.Interfaces;
using Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Models;

namespace Application.Files
{
    public class Add
    {
        public class Command : IRequest<Result<AppFile>>
        {
            public IFormFile File { get; set; }
        }    

        public class Handler : IRequestHandler<Command, Result<AppFile>>    
        {
            private readonly DataContext _ctx;
            
            private readonly IFileAccessor _fileAccessor;
            
            public Handler(DataContext ctx, IFileAccessor fileAccessor)
            {
                _ctx = ctx;
                _fileAccessor = fileAccessor;
            }
            
            public async Task<Result<AppFile>> Handle(Command request, CancellationToken cancellationToken)
            {
                var photoUploadResult = await _fileAccessor.AddFile(request.File);

                var file = new AppFile
                {
                    PublicId = photoUploadResult.PublicId,
                    Url = photoUploadResult.Url
                };

                _ctx.Files.Add(file);

                bool result = await _ctx.SaveChangesAsync() > 0;

                if (result)
                    return Result<AppFile>.Success(file);

                return Result<AppFile>.Failure("Возникла ошибка при загрузке файла");
            }
        }
    }
}