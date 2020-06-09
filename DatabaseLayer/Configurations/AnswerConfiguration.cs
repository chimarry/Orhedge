using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseLayer.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(x => x.StudentId).IsRequired();
            builder.Property(x => x.TopicId).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.BestAnswer).HasDefaultValue(false);
            builder.Property(x => x.Edited).HasDefaultValue(false);
            builder.Property(x => x.Deleted).HasDefaultValue(false);
        }
    }
}
