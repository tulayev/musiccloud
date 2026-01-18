using Application.Common.Interfaces.Repository;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class
        {
            return _dataContext.Set<TEntity>();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _dataContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _dataContext.Set<TEntity>().RemoveRange(entities);
        }

        public void DeleteRange<TEntity>(Func<TEntity, bool> where) where TEntity : class
        {
            _dataContext.Set<TEntity>().RemoveRange(_dataContext.Set<TEntity>().Where(where));
        }

        public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> rows) where TEntity : class
        {
            await _dataContext.Set<TEntity>().AddRangeAsync(rows);
        }

        public async Task AddAsync<TEntity>(TEntity row) where TEntity : class
        {
            await _dataContext.Set<TEntity>().AddAsync(row);
        }

        public async Task SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
