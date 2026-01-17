using Models;

namespace Application.Services.Users
{
    public interface IUserAccessorService
    {
        public User User { get; }
        public bool IsAuthenticated => User != null;
    }
}
