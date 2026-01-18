using Models;

namespace Application.Common.Interfaces.Users
{
    public interface IUserAccessorService
    {
        public User User { get; }
        public bool IsAuthenticated => User != null;
    }
}
