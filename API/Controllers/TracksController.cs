using Application.CQRS.Tracks.Commands;
using Application.CQRS.Tracks.Queries;
using Application.DTOs.Tracks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TracksController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResponse(await Mediator.Send(new GetTracksQuery()));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResponse(await Mediator.Send(new GetTrackByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrackDto createTrackDto)
        {
            return HandleResponse(await Mediator.Send(new CreateTrackCommand(createTrackDto)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, UpdateTrackDto updateTrackDto)
        {
            updateTrackDto.Id = id;
            return HandleResponse(await Mediator.Send(new UpdateTrackCommand(updateTrackDto)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResponse(await Mediator.Send(new DeleteTrackCommand(id)));
        }
    }
}
