using Application.Repository.IRepository;
using AutoMapper;
using Data;

namespace Application.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _ctx;

        public ITrackRepository TrackRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IFileRepository FileRepository { get; private set; }

        public UnitOfWork(DataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            TrackRepository = new TrackRepository(ctx, mapper);
            UserRepository = new UserRepository(ctx, mapper);
            FileRepository = new FileRepository(ctx, mapper);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}