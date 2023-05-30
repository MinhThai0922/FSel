using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;
using OrderAPI.Repositories.IRepon;
using static OrderAPI.Queries.OrderDetailQueries.OrderDetailQuery;

namespace OrderAPI.Handlers.OrderDetailHandlers
{
    public class GetByIdOrderDetailHandler : IRequestHandler<GetByIdOrderDetailQuery, List<OrderDetail>>
    {
        private readonly IOrderDetailRepon _iOrderDetail;
        private readonly IOrderRepon _iOrderRepon;

        public GetByIdOrderDetailHandler(IOrderDetailRepon iOrderDetail, IOrderRepon iOrderRepon)
        {
            _iOrderDetail = iOrderDetail;
            _iOrderRepon = iOrderRepon;
        }

        public async Task<List<OrderDetail>> Handle(GetByIdOrderDetailQuery request, CancellationToken cancellationToken)
        {
            //return await _context.OrderDetails.Where(p => p.OrderId == Id).ToListAsync();
            return await _iOrderDetail.GetOrderDetailByOrderId(request.Id);
        }
    }
}
