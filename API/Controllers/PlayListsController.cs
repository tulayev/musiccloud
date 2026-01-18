using Application.CQRS.PlayLists.Commands;
using Application.CQRS.PlayLists.Queries;
using Application.DTOs.PlayLists;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class PlayListsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResponse(await Mediator.Send(new GetPlayListsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResponse(await Mediator.Send(new GetPlayListByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlayList playList)
        {
            return HandleResponse(await Mediator.Send(new CreatePlayListCommand(playList)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, PlayList playList)
        {
            playList.Id = id;
            return HandleResponse(await Mediator.Send(new UpdatePlayListCommand(playList)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResponse(await Mediator.Send(new DeletePlayListCommand(id)));
        }

        [HttpPost("addtoplaylist")]
        public async Task<IActionResult> AddToPlayList(AddTrackToPlayListDto addTrackToPlayListDto)
        {
            return HandleResponse(await Mediator.Send(new AddTrackToPlayListCommand(addTrackToPlayListDto)));
        }
    }
}
