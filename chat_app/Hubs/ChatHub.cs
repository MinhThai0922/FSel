using Microsoft.AspNetCore.SignalR;

namespace chat_app.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string fromUser ,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage" ,fromUser, message);
        }
    }
}
