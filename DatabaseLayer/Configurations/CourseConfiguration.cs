using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Deleted).HasDefaultValue(false);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Semester).IsRequired();

        }
    }
}
