using MediatR;
using OrderAPI.Repositories.IRepon;
using OrderAPI.Repositories.Repon;
using OrderAPI.Services.IService;
using static OrderAPI.Commands.OrderCommands.OrderCommand;

namespace OrderAPI.Handlers.OrderHandlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, string>
    {
        public readonly IOrderService _orderService;
        public CreateCustomerHandler(IOrderService iOrderService)
        {
            _orderService = iOrderService;
        }
        public async Task<string> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            //var respone = await _orderService.AddCustomer(create);
            var respone = await _orderService.AddCustomer(request.model);
            return "thành công";
        }
    }
}
