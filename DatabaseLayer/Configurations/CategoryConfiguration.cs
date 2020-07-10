using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /// <summary>
        /// Adds configuration for Category, so that unique values are CourseId and Name, all values are required and attibute Deleted 
        /// has default value - false.
        /// </summary>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(x => new { x.CourseId, x.Name }).IsUnique();
            builder.Property(x => x.CourseId).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Deleted).HasDefaultValue(false);

        }
    }
}
