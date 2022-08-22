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

        public DbSet<Track> Tracks { get; set; }

        public DbSet<PlayList> PlayLists { get; set; }
        
        public DbSet<PlayListTrack> PlayListTrack { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PlayListTrack>(x => x.HasKey(pt => new { pt.Id }));
            builder.Entity<PlayListTrack>()
                .HasOne(p => p.PlayList)
                .WithMany(t => t.Tracks)
                .HasForeignKey(pt => pt.PlayListId);
            builder.Entity<PlayListTrack>()
                .HasOne(t => t.Track)
                .WithMany(p => p.PlayLists)
                .HasForeignKey(pt => pt.TrackId);
        }
    }
}