using System.Linq.Expressions;
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

        public async Task<T> Get(Expression<Func<T, bool>> predicate, string includes = null)
        {
            IQueryable<T> query = dbSet;

            if (includes != null)
            {
                foreach (string include in includes.Split(new char[] { ',' }, StringSplitOptions.TrimEntries))
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}