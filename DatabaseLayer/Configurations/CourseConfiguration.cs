using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Deleted).HasDefaultValue(false);
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
