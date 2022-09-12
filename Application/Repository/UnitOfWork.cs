using Application.Repository.IRepository;
using Data;

namespace Application.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _ctx;

        public UnitOfWork(DataContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class
        {
            return _ctx.Set<TEntity>();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _ctx.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _ctx.Set<TEntity>().RemoveRange(entities);
        }

        public void DeleteRange<TEntity>(Func<TEntity, bool> where) where TEntity : class
        {
            _ctx.Set<TEntity>().RemoveRange(_ctx.Set<TEntity>().Where(where));
        }

        public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> rows) where TEntity : class
        {
            await _ctx.Set<TEntity>().AddRangeAsync(rows);
        }

        public async Task AddAsync<TEntity>(TEntity row) where TEntity : class
        {
            await _ctx.Set<TEntity>().AddAsync(row);
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}