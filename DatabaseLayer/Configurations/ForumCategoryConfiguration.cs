using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Configurations
{
    public class ForumCategoryConfiguration : IEntityTypeConfiguration<ForumCategory>
    {
        public void Configure(EntityTypeBuilder<ForumCategory> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Order).IsRequired();
            builder.HasIndex(x => x.Order).IsUnique();
        }
    }
}
