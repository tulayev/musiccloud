using Application.Files;

namespace Application.Services
{
    public interface IFileAccessor
    {
        Task<FileUploadResult> AddFile(Stream stream, string filename);

        Task<string> DeleteFile(string publicId);
    }
}