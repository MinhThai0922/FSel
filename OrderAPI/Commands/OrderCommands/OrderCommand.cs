using MediatR;
using OrderAPI.Models;
using OrderAPI.ViewModel.CustomerViewModel;
using OrderAPI.ViewModel.OrderViewModel;

namespace OrderAPI.Commands.OrderCommands
{
    public class OrderCommand
    {
        public record CreateOrderCommand(CreateOrder orderDetail) : IRequest<Order>; 
        public record CreateCustomerCommand(CreateCustomer model) : IRequest<string>;
    }
}
