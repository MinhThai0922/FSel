using APICustomer.ViewModel.UserViewModel;

namespace CustomerAPI.Repositories.IRepon
{
    public interface IUserRepon
    {
        public Task<string> Login(LoginModel model);
    }
}
