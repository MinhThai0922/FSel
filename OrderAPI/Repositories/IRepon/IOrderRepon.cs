using OrderAPI.Models;
using OrderAPI.ViewModel.OrderViewModel;

namespace OrderAPI.Repositories.IRepon
{
    public interface IOrderRepon
    {
        Task<string> Create(CreateOrder create);
        Task<List<Order>> GetAll();
        Task<Order> GetById(Guid Id);
        Task<List<Order>> GetByIdKhachHang(Guid Id);
        Task UpdateTotalPrice(Guid OrderId);
        Task<Order> GetByIdOrder(Guid Id);
    }
}
