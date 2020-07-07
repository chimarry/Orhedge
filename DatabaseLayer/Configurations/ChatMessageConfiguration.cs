using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseLayer.Configurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        /// <summary>
        /// Adds configuration for ChatMessage, so that all values are required and attribute Deleted has default value - false.
        /// </summary>
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.Property(x => x.StudentId).IsRequired();
            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.ChatMessageId).IsRequired();
            builder.Property(x => x.SentOn).IsRequired();
            builder.Property(x => x.Deleted).HasDefaultValue(false);
        }
    }
}
