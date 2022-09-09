using System.Linq.Expressions;

namespace Application.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();

        Task<T> Get(Expression<Func<T, bool>> predicate, string includes = null);

        void Add(T entity);

        void Delete(T entity);
    }
}