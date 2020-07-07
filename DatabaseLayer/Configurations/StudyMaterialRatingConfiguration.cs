using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Configurations
{
    public class StudyMaterialRatingConfiguration : IEntityTypeConfiguration<StudyMaterialRating>
    {
        /// <summary>
        /// Adds configuration for StudyMaterialRating, so that one student can rate only one material, that is, combination 
        /// of attributes StudentId and StudyMaterialId must be unique and it makes primary key. Attribute Rating is required.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StudyMaterialRating> builder)
        {
            builder.HasKey(x => new { x.StudentId, x.StudyMaterialId });
            builder.Property(x => x.Rating).IsRequired();
        }
    }
}
