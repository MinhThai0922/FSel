using MyChat.Context;
using MyChat.Models;

namespace MyChat.Repositories
{
    public class RoomRepo : IRoomRepo
    {
        private readonly MyChatContext _chatContext;

        public RoomRepo()
        {
        }

        public RoomRepo(MyChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        public async Task<int> CreateRoom(Rooms rooms)
        {
            try
            {
                Rooms room = new Rooms()
                {
                    ID = Guid.NewGuid(),
                    RoomName = rooms.RoomName,
                    CreatedAt = DateTime.Now
                };
                //_chatContext.Rooms.AddAsync(rooms);
                _chatContext.SaveChanges();
                return 1;
            }catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<string> JoinRoom(Rooms room)
        {
            

            return "";
        }
    }
}
