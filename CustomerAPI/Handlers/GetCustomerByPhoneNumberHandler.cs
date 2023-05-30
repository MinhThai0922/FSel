using APICustomer.Models;
using CustomerAPI.Repositories.IRepon;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CustomerAPI.Queries.CustomerQuery;

namespace CustomerAPI.Handlers
{
    public class GetCustomerByPhoneNumberHandler : IRequestHandler<GetCustomerByPhoneNumberQuery, Customer>
    {
        private readonly ICustomerRepon _customerRepon;
        public GetCustomerByPhoneNumberHandler(ICustomerRepon customerRepon)
        {
            _customerRepon = customerRepon;
        }
        public async Task<Customer> Handle(GetCustomerByPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            //var customer = await _context.Customers.FirstOrDefaultAsync(p => p.PhoneNumber == phonenumber);
            var customer = await _customerRepon.GetByPhoneNumber(request.PhoneNumber);
            if (customer == null) return null;
            return customer;
        }
    }
}
