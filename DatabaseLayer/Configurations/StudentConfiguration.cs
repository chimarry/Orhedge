using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        /// <summary>
        /// Adds configuration for Student, where all attributes accept Description, Photo and PhotoVersion are required.
        /// Attibutes Username, Email, Index all come with unique constraint. 
        /// Attribute Deleted has default value false, while attribute Description is by default empty string.
        /// </summary>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Student> builder)
        {
            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Index).IsUnique();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Deleted).HasDefaultValue(false);
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.Privilege).IsRequired();
            builder.Property(x => x.Index).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.Salt).IsRequired();
            builder.Property(x => x.Description).HasDefaultValue(string.Empty);
        }
    }
}
