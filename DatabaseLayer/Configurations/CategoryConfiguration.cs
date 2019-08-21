using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {

            builder.Property(x => x.CourseId).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Deleted).HasDefaultValue(false);

        }
    }
}
