using Application.PlayLists;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class PlayListsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(PlayList playList)
        {
            return HandleResult(await Mediator.Send(new Create.Command { PlayList = playList }));
        }
    }
}