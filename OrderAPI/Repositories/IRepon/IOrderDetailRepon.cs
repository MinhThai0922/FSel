using OrderAPI.Models;
using OrderAPI.ViewModel.OrderDetailViewModel;

namespace OrderAPI.Repositories.IRepon
{
    public interface IOrderDetailRepon
    {
        Task<int> Create(OrderDetail orderDetail);
        Task<OrderDetail> GetByIdOrderDetail(Guid Id);
        Task<List<OrderDetail>> GetOrderDetailByOrderId(Guid Id);
        Task<int> CheckOrderDetail(OrderDetail orderDetail);
    }
}
