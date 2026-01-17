using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace Application.Services.Files
{
    public class FileAccessorService : IFileAccessorService
    {
        private readonly Cloudinary _cloudinary;

        public FileAccessorService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<FileUploadResult> AddFile(Stream stream, string filename)
        {
            var ext = Path.GetExtension(filename).ToLower();

            var uploadResult = ext == ".mp3"
                ? await UploadAudio(stream, filename)
                : await UploadImage(stream, filename);

            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            return new FileUploadResult(uploadResult.PublicId, uploadResult.SecureUrl.ToString());
        }

        public async Task<string> DeleteFile(string publicId)
        {
            var result = await _cloudinary.DestroyAsync(new DeletionParams(publicId));

            return result.Result == "ok" ? result.Result : null;
        }

        private async Task<RawUploadResult> UploadAudio(Stream stream, string filename)
        {
            return await _cloudinary.UploadAsync(new VideoUploadParams
            {
                File = new FileDescription(filename, stream)
            });
        }

        private async Task<RawUploadResult> UploadImage(Stream stream, string filename)
        {
            return await _cloudinary.UploadAsync(new ImageUploadParams
            {
                File = new FileDescription(filename, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill")
            });
        }
    }
}