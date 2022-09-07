using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.Configurations
{
    public class PlayListTrackConfig : IEntityTypeConfiguration<PlayListTrack>
    {
        public void Configure(EntityTypeBuilder<PlayListTrack> builder)
        {
            builder.HasKey(pt => new { pt.Id });

            builder.HasOne(p => p.PlayList)
                .WithMany(t => t.Tracks)
                .HasForeignKey(pt => pt.PlayListId);
            
            builder.HasOne(t => t.Track)
                .WithMany(p => p.PlayLists)
                .HasForeignKey(pt => pt.TrackId);
        }
    }
}