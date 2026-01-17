using Data;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Microsoft.AspNetCore.Identity;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddSignInManager<SignInManager<User>>();
        }
    }
}
