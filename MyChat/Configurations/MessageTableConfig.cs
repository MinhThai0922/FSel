using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyChat.Models;
using System.Security.Cryptography.X509Certificates;

namespace MyChat.Configurations
{
    public class MessageTableConfig : IEntityTypeConfiguration<MessageTables>
    {
        public void Configure(EntityTypeBuilder<MessageTables> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(c => c.Content);
            builder.Property(c => c.SentAt);
            builder.HasOne(c => c.Rooms).WithMany(c => c.MessageTables).HasForeignKey(c => c.RoomID);
            builder.HasOne(c => c.Users).WithMany(c => c.MessageTables).HasForeignKey(c => c.UserID);
        }
    }
}
