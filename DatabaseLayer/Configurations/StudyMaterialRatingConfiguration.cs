using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Configurations
{
    public class StudyMaterialRatingConfiguration : IEntityTypeConfiguration<StudyMaterialRating>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StudyMaterialRating> builder)
        {
            builder.HasKey(x => new { x.StudentId, x.StudyMaterialId });
            builder.Property(x => x.Rating).IsRequired();
        }
    }
}
