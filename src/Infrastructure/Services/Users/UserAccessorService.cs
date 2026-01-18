using System.Security.Claims;
using Application.Common.Interfaces.Users;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Models;

namespace Infrastructure.Services.Users
{
    public class UserAccessorService : IUserAccessorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _dataContext;
        private User? _user;

        public UserAccessorService(IHttpContextAccessor httpContextAccessor, 
            DataContext dataContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataContext = dataContext;
        }

        public User? User => _user ??= GetUser();

        private User? GetUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            
            if (httpContext == null)
            {
                return null;
            }

            var username = httpContext.User.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }
            
            return _dataContext.Users.FirstOrDefault(x => x.UserName == username);
        }
    }
}
