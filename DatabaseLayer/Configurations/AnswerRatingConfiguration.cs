using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Configurations
{
    public class AnswerRatingConfiguration : IEntityTypeConfiguration<AnswerRating>
    {
        public void Configure(EntityTypeBuilder<AnswerRating> builder)
        {
            builder.Property(x => x.Rating).IsRequired();
            builder.HasKey(x => new { x.AnswerId, x.StudentId });
        }
    }
}
