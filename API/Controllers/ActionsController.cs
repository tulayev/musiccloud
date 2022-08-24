using Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class ActionsController : ControllerBase
    {
        private readonly DataContext _ctx;

        public ActionsController(DataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost("api/addtoplaylist")]
        public async Task<IActionResult> AddToPlayList(Guid playListId, Guid trackId)
        {
            var playList = await _ctx.PlayLists.FindAsync(playListId);
            var track = await _ctx.Tracks.FindAsync(trackId);

            var playListTrack = new PlayListTrack 
            {
                PlayList = playList,
                Track = track
            };

            playList.Tracks.Add(playListTrack);

            bool result = await _ctx.SaveChangesAsync() > 0;

            if (result)
                return Ok();

            return BadRequest();
        }   
    }
}