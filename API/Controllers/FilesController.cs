using Application.CQRS.Files.Commands;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FilesController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] UploadFileCommand command)
        {
            return HandleResult(await Mediator.Send(command));
        }
    }
}