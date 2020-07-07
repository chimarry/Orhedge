using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        /// <summary>
        /// Adds configuration for Course, so that attribute Name is required with unique constraint, and attibute deleted 
        /// has default value - false.
        /// </summary>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Deleted).HasDefaultValue(false);
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => new { x.Name }).IsUnique();
        }
    }
}
