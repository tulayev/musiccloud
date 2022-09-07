using System.Linq.Expressions;
using Application.Repository.IRepository;
using AutoMapper;
using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext ctx, IMapper mapper) : base(ctx, mapper)
        {
        }

        public async Task<User> GetAuthorizedUser(Expression<Func<User, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }
    }
}