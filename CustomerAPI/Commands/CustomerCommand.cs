using APICustomer.Models;
using CustomerAPI.ViewModel.CustomerViewModel;
using MediatR;

namespace CustomerAPI.Commands
{
    public class CustomerCommand
    {
        public record CreateCustomerCommand(CreateCustomer model) : IRequest<Customer>;
        public record UpdateCustomerCommand(Guid Id, UpdateCustomer model) : IRequest<int>;
        public record DeleteCustomerCommand(Guid Id) : IRequest<int>;
    }
}
