using Application.DTOs;
using Models;

namespace Application.Repository.IRepository
{
    public interface ITrackRepository : IRepository<Track>
    {
        Task<List<TrackDTO>> GetWithRelatedData();

        Task<TrackDTO> GetWithRelatedData(Guid id);

        Task<Track> Update(TrackDTO trackDTO);
    }
}