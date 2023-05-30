using MediatR;
using Newtonsoft.Json;
using OrderAPI.Models;
using OrderAPI.Repositories.IRepon;
using OrderAPI.Repositories.Repon;
using OrderAPI.Services.IService;
using OrderAPI.ViewModel.OrderViewModel;
using static OrderAPI.Queries.OrderQueries.OrderQuery;

namespace OrderAPI.Handlers.OrderHandlers
{
    public class GetByIdKhachHangHandler : IRequestHandler<GetByIdKhachHangQuery, List<Order>>
    {
        private readonly IOrderRepon _iOrderRepon;
        public readonly OrderRepon orderRepon;

        public GetByIdKhachHangHandler(IOrderRepon orderRepon)
        {
            _iOrderRepon = orderRepon;
        }

        public async Task<List<Order>> Handle(GetByIdKhachHangQuery request, CancellationToken cancellationToken)
        {
            var response = await _iOrderRepon.GetByIdKhachHang(request.Id);
            return response.ToList();

        }
    }
}
