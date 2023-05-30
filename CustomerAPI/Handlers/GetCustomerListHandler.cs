using APICustomer.Models;
using APICustomer.ViewModel.CustomerViewModel;
using CustomerAPI.Repositories.IRepon;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CustomerAPI.Queries.CustomerQuery;

namespace CustomerAPI.Handlers
{
    public class GetCustomerListHandler : IRequestHandler<GetCustomerListQuery, List<Customer>>
    {
        private readonly ICustomerRepon _customerRepon;

        public GetCustomerListHandler(ICustomerRepon customerRepon)
        {
            _customerRepon = customerRepon;
        }
        public async Task<List<Customer>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        {
            var result = await _customerRepon.GetListCustomers();
            if (request.filter.FullName != null)
            {
                result = result.Where(p => p.FullName.Contains(request.filter.FullName)).ToList();
            }
            if (request.filter.Birthday != null)
            {
                result = result.Where(p => p.BirthDay.Day == request.filter.Birthday.Value.Day && p.BirthDay.Month == request.filter.Birthday.Value.Month && p.BirthDay.Year == request.filter.Birthday.Value.Year).ToList();
            }
            if (request.filter.PhoneNumber != null)
            {
                result = result.Where(p => p.PhoneNumber.Contains(request.filter.PhoneNumber)).ToList();
            }
            return result;

        }
    }
}
