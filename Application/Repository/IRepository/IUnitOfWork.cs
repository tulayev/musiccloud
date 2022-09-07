namespace Application.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ITrackRepository TrackRepository { get; }
        
        IUserRepository UserRepository { get; }
        
        IFileRepository FileRepository { get; } 

        Task SaveChanges();
    }
}