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

        public async Task<List<TrackDTO>> GetWithRelatedData()
        {
            return await dbSet
                .ProjectTo<TrackDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<TrackDTO> GetWithRelatedData(Guid id)
        {
            return await dbSet
                .ProjectTo<TrackDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public override void Add(Track track)
        {
            if (track.Audio != null)
                _ctx.Files.Attach(track.Audio);

            if (track.Poster != null)
                _ctx.Files.Attach(track.Poster);
            
            dbSet.Add(track);
        }

        public async Task<Track> Update(TrackDTO trackDTO)
        {
            var track = await dbSet.FirstOrDefaultAsync(t => t.Id == trackDTO.Id);

            if (track == )

            track.Title = request.Track.Title;
            track.Author = request.Track.Author;
            track.Genre = request.Track.Genre;
        }
    }
}