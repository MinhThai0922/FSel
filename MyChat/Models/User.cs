namespace MyChat.Models
{
    public class User 
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<Rooms> Rooms { get; set; }
        public ICollection<MessageTables> MessageTables { get; set; }   
    }
}
