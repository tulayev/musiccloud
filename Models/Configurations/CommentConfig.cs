using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.Configurations
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(t => t.Track)
                .WithMany(c => c.Comments)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}