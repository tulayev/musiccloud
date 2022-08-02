using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext ctx) 
        {
            if (ctx.Tracks.Any())
                return;

            var tracks = new List<Track>
            {
                new Track
                {
                    Title = "Byte",
                    Author = "Martin Garrix & Brooks",
                    Genre = "ElectroHouse"
                },
                new Track
                {
                    Title = "Take What You Want",
                    Author = "Post Malone, Ozzy Osbourne & Travi$ Scott",
                    Genre = "Hip-Hop"
                },
                new Track
                {
                    Title = "Marooned",
                    Author = "Pink Floyd",
                    Genre = "Rock"
                }
            };

            await ctx.Tracks.AddRangeAsync(tracks);
            await ctx.SaveChangesAsync();
        }
    }
}