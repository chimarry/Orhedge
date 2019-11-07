using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseLayer.Configurations
{
    class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.HasIndex(entity => entity.Email);
            builder.HasIndex(entity => entity.RegistrationCode).IsUnique();
            builder.Property(entity => entity.Timestamp).IsRequired();
            builder.Property(entity => entity.Used).IsRequired().HasDefaultValue(false);
        }
    }
}
