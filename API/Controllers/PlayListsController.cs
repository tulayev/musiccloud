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

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, PlayList playList)
        {
            playList.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { PlayList = playList }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}