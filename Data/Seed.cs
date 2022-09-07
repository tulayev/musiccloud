using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public static class Seed
    {
        public static void SeedInitialData(ModelBuilder builder) 
        {
            string email = "jon@test.com";
            string username = "jon";

            var jon = new User
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = username[0].ToString().ToUpper() + username.Substring(1),
                UserName = username,
                Email = email,
                EmailConfirmed = false,
                NormalizedEmail = email.ToUpper(),
                NormalizedUserName = username.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString().ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };

            var hasher = new PasswordHasher<User>();
            jon.PasswordHash = hasher.HashPassword(jon, "Pa$$w0rd");

            builder.Entity<User>().HasData(jon);

            var tracks = new List<Track>
            {
                new Track
                {
                    Id = Guid.NewGuid(),
                    Title = "Byte",
                    Author = "Martin Garrix & Brooks",
                    Genre = "ElectroHouse",
                    UserId = jon.Id
                },
                new Track
                {
                    Id = Guid.NewGuid(),
                    Title = "Take What You Want",
                    Author = "Post Malone, Ozzy Osbourne & Travi$ Scott",
                    Genre = "Hip-Hop",
                    UserId = jon.Id
                },
                new Track
                {
                    Id = Guid.NewGuid(),
                    Title = "Marooned",
                    Author = "Pink Floyd",
                    Genre = "Rock",
                    UserId = jon.Id
                }
            };

            builder.Entity<Track>().HasData(tracks);
        }
    }
}