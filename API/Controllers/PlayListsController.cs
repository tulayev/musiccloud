using Application.PlayLists;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class PlayListsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlayList playList)
        {
            return HandleResult(await Mediator.Send(new Create.Command { PlayList = playList }));
        }
    }
}