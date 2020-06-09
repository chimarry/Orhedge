using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseLayer.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Deleted).HasDefaultValue(false);
            builder.Property(x => x.Edited).HasDefaultValue(false);
        }
    }
}
