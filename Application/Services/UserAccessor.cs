using System.Security.Claims;
using Data;
using Microsoft.AspNetCore.Http;
using Models;

namespace Application.Services
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly DataContext _ctx;

        private User _user;

        public UserAccessor(IHttpContextAccessor httpContextAccessor, DataContext ctx)
        {
            _httpContextAccessor = httpContextAccessor;
            _ctx = ctx;
        }

        public User User => _user ??= GetUser();

        private User GetUser()
        {
            string username = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Name);
            return _ctx.Users.FirstOrDefault(u => u.UserName == username);
        }
    }
}