using MediatR;
using Newtonsoft.Json;
using OrderAPI.Services.IService;
using OrderAPI.ViewModel.OrderViewModel;
using static OrderAPI.Queries.OrderQueries.OrderQuery;

namespace OrderAPI.Handlers.OrderHandlers
{
    public class GetCustomByPhoneNumber : IRequestHandler<FindCustomerQuery, string>
    {
        public readonly IOrderService _orderService;
        public GetCustomByPhoneNumber(IOrderService iOrderService)
        {
            _orderService = iOrderService;
        }
        public async Task<string> Handle(FindCustomerQuery request, CancellationToken cancellationToken)
        {

            var response = await _orderService.GetCustomer(request.phonenumber);
            if (response == "Không tìm thấy") 
                return null;

            var customer = JsonConvert.DeserializeObject<ViewModelCustomer>(response);
            return "1";
        }
    }
}
