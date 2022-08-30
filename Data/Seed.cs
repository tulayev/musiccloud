using Microsoft.AspNetCore.Identity;
using Models;

namespace Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext ctx, UserManager<User> userManager) 
        {
            var jon = new User { DisplayName = "Jon", UserName = "jon", Email = "jon@test.com" };

            if (!userManager.Users.Any()) 
            {
                var users = new List<User>
                {
                    jon,
                    new User { DisplayName = "Tom", UserName = "tom", Email = "tom@test.com" },
                    new User { DisplayName = "Rob", UserName = "rob", Email = "rob@test.com" }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (ctx.Tracks.Any())
                return;

            var tracks = new List<Track>
            {
                new Track
                {
                    Title = "Byte",
                    Author = "Martin Garrix & Brooks",
                    Genre = "ElectroHouse",
                    User = jon
                },
                new Track
                {
                    Title = "Take What You Want",
                    Author = "Post Malone, Ozzy Osbourne & Travi$ Scott",
                    Genre = "Hip-Hop",
                    User = jon
                },
                new Track
                {
                    Title = "Marooned",
                    Author = "Pink Floyd",
                    Genre = "Rock",
                    User = jon
                }
            };

            await ctx.Tracks.AddRangeAsync(tracks);
            await ctx.SaveChangesAsync();
        }
    }
}