using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;
using OrderAPI.Repositories.IRepon;
using OrderAPI.Repositories.Repon;
using static OrderAPI.Commands.OrderCommands.OrderCommand;

namespace OrderAPI.Handlers.OrderHandlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepon _iOrderRepon;
        public readonly OrderRepon orderRepon;
        public CreateOrderHandler(IOrderRepon iOrderRepon)
        {
            _iOrderRepon = iOrderRepon;
        }
        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Order order = new Order()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = request.orderDetail.CustomerId,
                    OrderDate = DateTime.Now,
                    TotalPrice = 0,
                };
                //await _context.Orders.AddAsync(order);
                //await _context.SaveChangesAsync();
                try
                {
                    foreach (var item in request.orderDetail.orderDetail)
                    {
                        OrderDetail orderDetail = new OrderDetail()
                        {
                            Id = Guid.NewGuid(),
                            OrderId = order.Id,
                            ProductName = item.ProductName,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                        };
                        //await _context.OrderDetails.AddAsync(orderDetail);
                        //await _context.SaveChangesAsync();
                    }
                    await orderRepon.UpdateTotalPrice(order.Id);
                    return order;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
