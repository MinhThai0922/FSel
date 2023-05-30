using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Models;
using OrderAPI.Repositories.IRepon;
using OrderAPI.ViewModel.OrderDetailViewModel;
using OrderAPI.ViewModel.OrderViewModel;
using static OrderAPI.Commands.OrderCommands.OrderCommand;
using static OrderAPI.Commands.OrderDetailCommands.OrderDeatailCommand;
using static OrderAPI.Queries.OrderDetailQueries.OrderDetailQuery;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepon _repon;
        private readonly IMediator _mediator;

        public OrderDetailController(IOrderDetailRepon repon)
        {
            _repon = repon;
        }
        [HttpPost]
        [Route("CreateOrderDetail")]
        public async Task<IActionResult> Create([FromBody] CreateOrderDetail orderDetail)
        {
            //var result = await _repon.Create(orderDetail);
            var result = await _mediator.Send(new CreateOrderDetailCommand(orderDetail));
            if (result == 0) return BadRequest("Không tìm thấy hóa đơn");
            if (result == 1) return BadRequest("tạo mới thành công");
            if (result == 3) return BadRequest("Đã xảy ra lỗi, vui lòng thử lại");
            return Ok("Thêm thành công");
        }
        [HttpGet]
        [Route("GetByIdOderDetail/{Id}")]
        public async Task<IActionResult> GetOrderDetailById(string Id)
        {
            //var result = await _repon.GetByIdOrder(Guid.Parse(Id));
            //return Ok(result);
            var result = await _mediator.Send(new GetByIdOrderDetailQuery(Guid.Parse(Id)));
            return Ok(result);
        }
    }
}
