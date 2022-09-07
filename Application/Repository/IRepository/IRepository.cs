namespace Application.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();

        void Add(T entity);

        void Delete(T entity);
    }
}