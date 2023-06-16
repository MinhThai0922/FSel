using Microsoft.EntityFrameworkCore;
using MyChat.Context;
using MyChat.Models;
using MyChat.ViewModel;

namespace MyChat.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly MyChatContext _chatContext;
        public UserRepo(MyChatContext chatContext)
        {
            _chatContext = chatContext;
        }
        public async Task<User> Login(LoginModel model)
        {
            var user = await _chatContext.Users.FirstOrDefaultAsync(c => c.UserName == model.UserName && c.Password == model.Password);
            return user;
        }
    }
}
