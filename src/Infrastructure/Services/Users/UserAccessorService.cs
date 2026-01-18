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
        private User _user;

        public UserAccessorService(IHttpContextAccessor httpContextAccessor, 
            DataContext dataContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataContext = dataContext;
        }

        public User User => _user ??= GetUser();

        private User GetUser()
        {
            var username = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Name);
            
            return _dataContext.Users.FirstOrDefault(u => u.UserName == username);
        }
    }
}