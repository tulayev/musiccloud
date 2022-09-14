using Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Track).Assembly);
            Seed.SeedInitialData(modelBuilder);
        }

        public DbSet<AppFile> Files { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<PlayList> PlayLists { get; set; }
        
        public DbSet<PlayListTrack> PlayListTrack { get; set; }
        
        public DbSet<Comment> Comments { get; set; }
    }
}