namespace MyChat.Models
{
    public class Rooms
    {
        public Guid ID { get; set; }
        public string RoomName { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<MessageTables> MessageTables { get; set; }
    }
}
