using Application.CQRS.Files;

namespace Application.Services.Files
{
    public interface IFileAccessorService
    {
        Task<FileUploadResult> AddFile(Stream stream, string filename);

        Task<string> DeleteFile(string publicId);
    }
}