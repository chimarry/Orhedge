using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
