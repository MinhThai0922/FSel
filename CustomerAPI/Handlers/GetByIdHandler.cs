using APICustomer.Models;
using CustomerAPI.Repositories.IRepon;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CustomerAPI.Queries.CustomerQuery;

namespace CustomerAPI.Handlers
{
    public class GetByIdHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly ICustomerRepon _customerRepon;
        public GetByIdHandler(ICustomerRepon customerRepon)
        {
            _customerRepon = customerRepon;
        }
        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepon.GetById(request.Id);
            if (customer == null) return null;
            return customer;
        }
    }
}
