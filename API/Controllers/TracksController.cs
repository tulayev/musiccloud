using Application.Tracks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class TracksController : BaseApiController
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
        public async Task<IActionResult> Create(Track track)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Track = track }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, Track track)
        {
            track.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Track = track }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}