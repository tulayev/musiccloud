using Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Models;

namespace Application.CQRS.Files.Commands
{
    public record UploadFileCommand(IFormFile File) : IRequest<ApiResponse<AppFile>>;
}
