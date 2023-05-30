using APICustomer.DatabaseContext;
using APICustomer.Models;
using CustomerAPI.Repositories.IRepon;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CustomerAPI.Commands.CustomerCommand;

namespace CustomerAPI.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepon _customerRepon;
        public CreateCustomerHandler(ICustomerRepon customerRepon)
        {
            _customerRepon = customerRepon;
        }
        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var checkemail = await (_customerRepon.CheckEmail(request.model.Email));
                if (checkemail == false)
                    return null;
                var checkphone = await (_customerRepon.CheckPhoneNumber(request.model.PhoneNumber));
                if (checkphone == false)
                    return null;
                Customer customer = new Customer()
                {
                    Id = Guid.NewGuid(),
                    Address = request.model.Address,
                    BirthDay = request.model.BirthDay,
                    Email = request.model.Email,
                    PhoneNumber = request.model.PhoneNumber,
                    FullName = request.model.FullName
                };
                if(await _customerRepon.Create(customer) == "thêm thành công")
                {
                    return customer;
                }
                return null; 
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
