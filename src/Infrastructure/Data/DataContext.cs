using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<AppFile> Files { get; set; } = null!;
        public DbSet<Track> Tracks { get; set; } = null!;
        public DbSet<PlayList> PlayLists { get; set; } = null!;
        public DbSet<PlayListTrack> PlayListTrack { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Track).Assembly);
            Seed.SeedInitialData(modelBuilder);
        }
    }
}
