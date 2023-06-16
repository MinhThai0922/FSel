using MyChat.Models;
using MyChat.ViewModel;

namespace MyChat.Repositories
{
    public interface IUserRepo
    {
        Task<User> Login(LoginModel model);
    }
}
