using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Configurations
{
    public class StudyMaterialConfiguration : IEntityTypeConfiguration<StudyMaterial>
    {
        /// <summary>
        /// Adds configuration for StudyMaterial, so that all the attributes are required, and Uri must be unique. Also, 
        /// attribute deleted has default value - false.
        /// </summary>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StudyMaterial> builder)
        {
            builder.HasIndex(x => x.Uri).IsUnique();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.StudentId).IsRequired();
            builder.Property(x => x.UploadDate).IsRequired();
            builder.Property(x => x.Uri).IsRequired();
            builder.Property(x => x.Deleted).HasDefaultValue(false);
        }
    }
}
