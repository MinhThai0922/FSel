using MediatR;
using OrderAPI.Models;

namespace OrderAPI.Queries.OrderQueries
{
    public class OrderQuery
    {
        public record FindCustomerQuery(string phonenumber) : IRequest<string>;
        public record GetByIdOrderQuery(Guid Id) : IRequest<Order>;
        public record GetByIdKhachHangQuery(Guid Id) : IRequest<List<Order>>;
        public record GetAllListOrderQuery() : IRequest<List<Order>>;
    }
}
