using Application.Interfaces;
using Application.Files;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Files
{
    public class FileAccessor : IFileAccessor
    {
        private readonly Cloudinary _cloudinary;

        public FileAccessor(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<FileUploadResult> AddFile(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                await using var stream = formFile.OpenReadStream();

                string ext = Path.GetExtension(formFile.FileName).ToLower();

                RawUploadResult uploadResult;

                if (ext == ".mp3")
                {
                    uploadResult = await _cloudinary.UploadAsync(new VideoUploadParams
                    {
                        File = new FileDescription(formFile.FileName, stream)
                    });
                }
                else 
                {
                    uploadResult = await _cloudinary.UploadAsync(new ImageUploadParams
                    {
                        File = new FileDescription(formFile.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                    });
                }

                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }

                return new FileUploadResult
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.SecureUrl.ToString()
                };
            }

            return null;
        }

        public async Task<string> DeleteFile(string publicId)
        {
            var result = await _cloudinary.DestroyAsync(new DeletionParams(publicId));
            return result.Result == "ok" ? result.Result : null;
        }
    }
}