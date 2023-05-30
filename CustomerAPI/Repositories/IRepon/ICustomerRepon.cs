using APICustomer.Models;
using APICustomer.ViewModel.CustomerViewModel;

namespace CustomerAPI.Repositories.IRepon
{
    public interface ICustomerRepon
    {
        Task<bool> CheckEmail(string email);
        Task<bool> CheckPhoneNumber(string phone);
        Task<List<Customer>> GetListCustomers();
        Task<string> Create(Customer customer);
        Task<int> Update(Guid Id, UpdateCustomer model);
        Task<int> Delete(Customer customer);
        Task<Customer> GetById(Guid Id);
        Task<Customer> GetByPhoneNumber(string phonenumber);
        Task<bool> CheckUpdatePhoneNumber(Guid Id, string phone);
        Task<bool> CheckUpdateEmail(Guid Id, string email);
    }
}
