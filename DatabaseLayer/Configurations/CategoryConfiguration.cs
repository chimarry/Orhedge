using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(x => new { x.CourseId, x.Name }).IsUnique();
            builder.Property(x => x.CourseId).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Deleted).HasDefaultValue(false);

        }
    }
}
