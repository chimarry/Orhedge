﻿using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Configurations
{
    public class StudyMaterialConfiguration : IEntityTypeConfiguration<StudyMaterial>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StudyMaterial> builder)
        {
            builder.HasIndex(x => x.Uri).IsUnique();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.StudentId).IsRequired();
            builder.Property(x => x.UploadDate).IsRequired();
            builder.Property(x => x.Uri).IsRequired();
            builder.Property(x => x.Deleted).HasDefaultValue(false);
        }
    }
}
