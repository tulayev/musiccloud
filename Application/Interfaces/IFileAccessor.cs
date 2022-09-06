using Application.Files;

namespace Application.Interfaces
{
    public interface IFileAccessor
    {
        Task<FileUploadResult> AddFile(Stream stream, string filename);

        Task<string> DeleteFile(string publicId);
    }
}