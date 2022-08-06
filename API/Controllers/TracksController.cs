using Application.Tracks;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TracksController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Track>>> Get()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Track>> Get(Guid id)
        {
            return await Mediator.Send(new Details.Query{ Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Track track)
        {
            return Ok(await Mediator.Send(new Create.Command{ Track = track }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, Track track)
        {
            track.Id = id;
            return Ok(await Mediator.Send(new Edit.Command{ Track = track }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command{ Id = id }));
        }
    }
}