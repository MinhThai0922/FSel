using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyChat.Models;

namespace MyChat.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(c => c.Name);
            builder.Property(c => c.UserName);
            builder.Property(c => c.Password);
            builder.HasMany(c => c.Rooms).WithMany(c => c.Users);
            builder.HasMany(c => c.MessageTables).WithOne(c => c.Users).HasForeignKey(c => c.UserID);
        }
    }
}
