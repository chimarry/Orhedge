using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseLayer.Configurations
{
    class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
    {
        /// <summary>
        /// Adds configuration for registration, so that all the attributes are required except Email and Registration code, where 
        /// Registration code has unique constraind, and attribute Used has default value - false.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.HasIndex(entity => entity.Email);
            builder.HasIndex(entity => entity.RegistrationCode).IsUnique();
            builder.Property(entity => entity.Timestamp).IsRequired();
            builder.Property(entity => entity.Used).IsRequired().HasDefaultValue(false);
        }
    }
}
