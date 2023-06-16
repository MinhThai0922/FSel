namespace MyChat.Models
{
    public class MessageTables
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid RoomID { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public Rooms Rooms { get; set; }
        public User Users { get; set; }
    }
}
