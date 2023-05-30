using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ocelot.Responses;
using OrderAPI.Models;
using OrderAPI.Queries.OrderQueries;
using OrderAPI.Repositories.IRepon;
using OrderAPI.Repositories.Repon;
using OrderAPI.Services.IService;
using OrderAPI.ViewModel.OrderViewModel;
using static OrderAPI.Queries.OrderQueries.OrderQuery;

namespace OrderAPI.Handlers.OrderHandlers
{
    public class GetByIdOrderHandler : IRequestHandler<GetByIdOrderQuery, Order>
    {
        private readonly IOrderRepon _iOrderRepon;
        public readonly IOrderDetailRepon _orderDetailRepon;
        private readonly IOrderService _orderService;

        public GetByIdOrderHandler(IOrderRepon orderRepon, IOrderDetailRepon orderDetailRepon, IOrderService orderService)
        {
            _iOrderRepon = orderRepon;
            _orderDetailRepon = orderDetailRepon;
            _orderService = orderService;
        }
        public async Task<Order> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
        {
            Order order = await _iOrderRepon.GetByIdOrder(request.Id);
            if (order == null) return null;
            List<OrderDetail> orderDetails = await _orderDetailRepon.GetOrderDetailByOrderId(request.Id);
            order.OrderDetails = orderDetails;
            return order;
        }
    }
}
