using APICustomer.Models;
using APICustomer.ViewModel.CustomerViewModel;
using MediatR;

namespace CustomerAPI.Queries
{
    public class CustomerQuery
    {
        public record GetCustomerListQuery(FilterCustomer filter) : IRequest<List<Customer>>;
        public record GetCustomerByIdQuery(Guid Id) : IRequest<Customer>;
        public record GetCustomerByPhoneNumberQuery(string PhoneNumber) : IRequest<Customer>;
    }
}
