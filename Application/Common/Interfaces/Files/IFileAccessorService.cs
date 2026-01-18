namespace Application.Common.Interfaces.Files
{
    public record FileUploadResult(string PublicId, string Url);

    public interface IFileAccessorService
    {
        Task<FileUploadResult> AddFile(Stream stream, string filename);
        Task<string> DeleteFile(string publicId);
    }
}