using Models;

namespace Application.Services
{
    public interface IUserAccessor
    {
        public User User { get; }

        public bool IsAuthenticated => User != null;
    }
}