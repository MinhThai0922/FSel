using MyChat.Models;
using MyChat.ViewModel;

namespace MyChat.Repositories
{
    public interface IRoomRepo
    {
        Task<int> CreateRoom(Rooms rooms);
    }
}
