using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;
using OrderAPI.Repositories.IRepon;
using OrderAPI.Repositories.Repon;
using OrderAPI.ViewModel.OrderDetailViewModel;
using static OrderAPI.Commands.OrderDetailCommands.OrderDeatailCommand;
using static OrderAPI.Queries.OrderDetailQueries.OrderDetailQuery;

namespace OrderAPI.Handlers.OrderDetailHandlers
{
    public class CreateOrderDetailHandler : IRequestHandler<CreateOrderDetailCommand, int>
    {
        private readonly IOrderDetailRepon _iOrderDetailRepon;
        private readonly IOrderRepon _iOrderRepon;

        public CreateOrderDetailHandler(IOrderDetailRepon iOrderDetailRepon, IOrderRepon iOrderRepon)
        {
            _iOrderDetailRepon = iOrderDetailRepon;
            _iOrderRepon = iOrderRepon;
        }

        public async Task<int> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _iOrderRepon.GetByIdOrder(request.createOrderDetail.OrderId);
                if (order == null) return 0;


                OrderDetail orderDetail = new OrderDetail()
                {
                    Id = Guid.NewGuid(),
                    OrderId = request.createOrderDetail.OrderId,
                    ProductName = request.createOrderDetail.ProductName,
                    Quantity = request.createOrderDetail.Quantity,
                    UnitPrice = request.createOrderDetail.UnitPrice,
                };
                await _iOrderDetailRepon.CheckOrderDetail(orderDetail);
                //await _context.OrderDetails.AddAsync(orderDetail);
                await _iOrderDetailRepon.Create(orderDetail);
                //await _context.SaveChangesAsync();
                //await _orderRepon.UpdateTotalPrice(create.OrderId);
                return 2;
            }
            catch (Exception ex)
            {
                return 3;
            }
        }
    }
}
