using Application.DTOs;
using Application.Repository.IRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Repository
{
    public class TrackRepository : Repository<Track>, ITrackRepository
    {
        private readonly IMapper _mapper;

        private readonly DataContext _ctx;

        public TrackRepository(DataContext ctx, IMapper mapper) : base(ctx, mapper)
        {
            _mapper = mapper;
            _ctx = ctx;
        }

        public async Task<List<TrackDTO>> GetAll()
        {
            return await dbSet
                .ProjectTo<TrackDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public override void Add(Track track)
        {
            if (track.Audio != null)
                _ctx.Files.Attach(track.Audio);

            if (track.Poster != null)
                _ctx.Files.Attach(track.Poster);
            
            dbSet.Add(track);
        }
    }
}