using Application.Files;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IFileAccessor
    {
        Task<FileUploadResult> AddFile(IFormFile formFile);

        Task<string> DeleteFile(string publicId);
    }
}