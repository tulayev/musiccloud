using Application.Repository.IRepository;
using AutoMapper;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _ctx;

        private readonly IMapper _mapper;
        
        protected readonly DbSet<T> dbSet;

        public Repository(DataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
            dbSet = ctx.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return await dbSet.ToListAsync();
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}