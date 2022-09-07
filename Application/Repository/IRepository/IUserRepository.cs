using System.Linq.Expressions;
using Models;

namespace Application.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetAuthorizedUser(Expression<Func<User, bool>> predicate);        
    }
}