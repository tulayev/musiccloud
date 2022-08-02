using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class TracksController : BaseApiController
    {
        private readonly DataContext _ctx;

        public TracksController(DataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public async Task<ActionResult<List<Track>>> Get()
        {
            return await _ctx.Tracks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Track>> Get(Guid id)
        {
            return await _ctx.Tracks.FindAsync(id);
        }
    }
}