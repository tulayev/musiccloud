using Application.Files;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FilesController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] Add.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }
    }
}