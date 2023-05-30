using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;
using OrderAPI.Repositories.IRepon;
using OrderAPI.Repositories.Repon;
using static OrderAPI.Queries.OrderQueries.OrderQuery;

namespace OrderAPI.Handlers.OrderHandlers
{
    public class GetAllListOrderHandler : IRequestHandler<GetAllListOrderQuery, List<Order>>
    {
        private readonly IOrderRepon _iOrderRepon;
        public readonly OrderRepon orderRepon;
        public GetAllListOrderHandler(IOrderRepon iOrderRepon)
        {
            _iOrderRepon = iOrderRepon;
        }

        public async Task<List<Order>> Handle(GetAllListOrderQuery request, CancellationToken cancellationToken)
        {
            //return await _context.Orders.ToListAsync();
            return await _iOrderRepon.GetAll();
        }
    }
}
