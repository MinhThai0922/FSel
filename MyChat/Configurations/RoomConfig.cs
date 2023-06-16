using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyChat.Models;

namespace MyChat.Configurations
{
    public class RoomConfig : IEntityTypeConfiguration<Rooms>
    {
        public void Configure(EntityTypeBuilder<Rooms> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(c => c.RoomName);
            builder.Property(c => c.CreatedAt);
            builder.HasMany(c => c.Users).WithMany(c => c.Rooms);
            builder.HasMany(c => c.MessageTables).WithOne(c => c.Rooms).HasForeignKey(c => c.RoomID);
        }
    }
}
