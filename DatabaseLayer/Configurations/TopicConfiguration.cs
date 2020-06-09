using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseLayer.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.Property(x => x.StudentId).IsRequired();
            builder.Property(x => x.ForumCategoryId).IsRequired();
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Locked).HasDefaultValue(false);
            builder.Property(x => x.Deleted).HasDefaultValue(false);
            builder.Property(x => x.LastPost).IsRequired();

        }
    }
}
