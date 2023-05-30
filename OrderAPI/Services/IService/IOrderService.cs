using OrderAPI.ViewModel.CustomerViewModel;
using Refit;

namespace OrderAPI.Services.IService
{
    public interface IOrderService
    {
        [Get("/api/Customers/GetByPhoneNumber/{phonenumber}")]
        Task<string> FindCustomerByPhoneNumber(string phoneNumber);
        [Post("/api/Customers/CreateCustomer")]
        Task<string> AddCustomer(CreateCustomer create);
        [Get("/api/Customers/GetById/{Id}")]
        Task<string> GetCustomer(string Id);
    }
}
