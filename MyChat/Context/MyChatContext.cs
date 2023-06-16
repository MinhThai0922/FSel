using Microsoft.EntityFrameworkCore;
using MyChat.Configurations;
using MyChat.Models;

namespace MyChat.Context
{
    public class MyChatContext : DbContext
    {
        public MyChatContext()
        {
        }
        public MyChatContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-KULFO3T\\MINHTHAI;Database=MyChat;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoomConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new MessageTableConfig());
        }
        public DbSet<User> Users { get; set; } 
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<MessageTables> MessageTables { get; set; }
    }
}
