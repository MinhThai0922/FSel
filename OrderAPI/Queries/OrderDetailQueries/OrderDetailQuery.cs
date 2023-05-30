using MediatR;
using OrderAPI.Models;

namespace OrderAPI.Queries.OrderDetailQueries
{
    public class OrderDetailQuery
    {
        public record GetByIdOrderDetailQuery(Guid Id) : IRequest<List<OrderDetail>>;
    }
}
