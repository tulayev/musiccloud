using Application.Core;
using Application.Interfaces;
using Application.Repository.IRepository;
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
            private readonly IUnitOfWork _unitOfWork;
         
            private readonly IFileAccessor _fileAccessor;
            
            public Handler(IUnitOfWork unitOfWork, IFileAccessor fileAccessor)
            {
                _unitOfWork = unitOfWork;
                _fileAccessor = fileAccessor;
            }
            
            public async Task<Result<AppFile>> Handle(Command request, CancellationToken cancellationToken)
            {
                await using var stream = request.File.OpenReadStream();

                var fileUploadResult = await _fileAccessor.AddFile(stream, request.File.FileName);

                var file = new AppFile
                {
                    PublicId = fileUploadResult.PublicId,
                    Url = fileUploadResult.Url
                };

                await _unitOfWork.AddAsync(file);
                await _unitOfWork.SaveChangesAsync();

                return Result<AppFile>.Success(file);
            }
        }
    }
}