using Application.DTOs;
using Models;

namespace Application.Repository.IRepository
{
    public interface ITrackRepository : IRepository<Track>
    {
        Task<List<TrackDTO>> GetAll();
    }
}