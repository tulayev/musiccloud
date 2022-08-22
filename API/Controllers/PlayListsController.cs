using Application.PlayLists;
using Domain;
using Microsoft.AspNetCore.Mvc;

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