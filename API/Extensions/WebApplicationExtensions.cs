using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> MigrateDatabaseAsync<T>(this WebApplication app) where T : DbContext
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<T>();
                    await db.Database.MigrateAsync();
                    await Seed.SeedData(db as DataContext);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return app;
        }
    }
}