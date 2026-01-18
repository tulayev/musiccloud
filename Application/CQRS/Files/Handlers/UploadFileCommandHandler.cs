using Application.Common.Interfaces.Files;
using Application.Common.Interfaces.Repository;
using Application.CQRS.Files.Commands;
using Application.Helpers;
using MediatR;
using Models;

namespace Application.CQRS.Files.Handlers
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, ApiResponse<AppFile>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileAccessorService _fileAccessor;

        public UploadFileCommandHandler(IUnitOfWork unitOfWork, IFileAccessorService fileAccessor)
        {
            _unitOfWork = unitOfWork;
            _fileAccessor = fileAccessor;
        }

        public async Task<ApiResponse<AppFile>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
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

            return ApiResponse<AppFile>.Success(file);
        }
    }
}