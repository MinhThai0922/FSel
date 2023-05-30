using CustomerAPI.Repositories.IRepon;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CustomerAPI.Commands.CustomerCommand;

namespace CustomerAPI.Handlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, int>
    {
        private readonly ICustomerRepon _customerRepon;
        public UpdateCustomerHandler(ICustomerRepon customerRepon)
        {
            _customerRepon = customerRepon;
        }
        public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var customer = await _context.Customers.FindAsync(Id);
                var customer = await _customerRepon.GetById(request.Id);
                if (customer == null) return 0;
                var checkemail = await (_customerRepon.CheckUpdateEmail(request.Id, request.model.Email));
                if (checkemail == false) return 1;
                var checkphone = await (_customerRepon.CheckUpdatePhoneNumber(request.Id, request.model.PhoneNumber));
                if (checkphone == false) return 2;
                customer.FullName = request.model.FullName;
                customer.Email = request.model.Email;
                customer.PhoneNumber = request.model.PhoneNumber;
                customer.BirthDay = request.model.BirthDay;
                customer.Address = request.model.Address;
                return 3;
            }
            catch (Exception ex)
            {
                return 4;
            }
        }
    }
}
