using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Configurations
{
    public class DiscussionPostConfiguration : IEntityTypeConfiguration<DiscussionPost>
    {
        public void Configure(EntityTypeBuilder<DiscussionPost> builder)
        {
            builder.Property(x => x.StudentId).IsRequired();
            builder.Property(x => x.TopicId).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Deleted).HasDefaultValue(false);
            builder.Property(x => x.Edited).HasDefaultValue(false);
        }
    }
}
