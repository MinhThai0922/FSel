using MediatR;
using OrderAPI.Models;
using OrderAPI.ViewModel.OrderDetailViewModel;
using OrderAPI.ViewModel.OrderViewModel;

namespace OrderAPI.Commands.OrderDetailCommands
{
    public class OrderDeatailCommand
    {
        public record CreateOrderDetailCommand(CreateOrderDetail createOrderDetail) : IRequest<int>;
    }
}
