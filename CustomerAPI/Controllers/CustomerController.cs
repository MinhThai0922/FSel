using APICustomer.Models;
using APICustomer.ViewModel.CustomerViewModel;
using CustomerAPI.Handlers;
using CustomerAPI.Repositories.IRepon;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CustomerAPI.Commands.CustomerCommand;
using static CustomerAPI.Queries.CustomerQuery;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepon _customerRepon;
        private readonly IMediator _mediator;
        public CustomersController(ICustomerRepon customerRepon, IMediator mediator)
        {
            _customerRepon = customerRepon;
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Policy = "view_customer_only")]
        public async Task<IActionResult> Get(int page, int pagesize, [FromQuery] FilterCustomer filter)
        {
            // lấy danh sách bản ghi
            var lstCustomer = await _mediator.Send(new GetCustomerListQuery(filter));
            // lấy tổng số lượng bản ghi
            int totalItems = lstCustomer.Count;
            // tính toán xem có bao nhiêu trang trên số lượng bản ghi
            int totalPages = (int)Math.Ceiling((double)totalItems / pagesize);
            // tính toán chỉ số của item đầu tiên trên trang hiện tại
            int skip = (page - 1) * pagesize;
            // chọn ra các item cho trang hiện tại bằng cách sử dụng phương thức Skip và Take của LINQ
            var pagedItems = lstCustomer.Skip(skip).Take(pagesize);

            // trả về danh sách item đã chọn và các thông tin về phân trang
            var response = new
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                PageSize = pagesize,
                CurrentPage = page,
                Items = pagedItems
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("CreateCustomer")]
        [Authorize(Policy = "create_customer_only")]
        public async Task<IActionResult> Create([FromBody] CreateCustomer model)
        {
            //var result = await _customerRepon.Create(model);
            var result = await _mediator.Send(new CreateCustomerCommand(model));
            if (result == null) return BadRequest("Đã xảy ra lỗi, vui lòng thử lại");
            return Ok(result);
        }

        [HttpPut]
        [Route("Update/{Id}")]
        [Authorize(Policy = "update_customer_only")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] UpdateCustomer model)
        {
            //var result = await _customerRepon.Update(Id, model);
            var result = await _mediator.Send(new UpdateCustomerCommand(Id, model));  
            if (result == 0)
            {
                return BadRequest("Không tìm thấy Customer");
            }
            else if (result == 1)
            {
                return BadRequest("Email đã được sử dụng");
            }
            else if (result == 2)
            {
                return BadRequest("Số điện thoại đã được sử dụng");
            }
            else if (result == 4)
            {
                return BadRequest("Đã xảy ra lỗi, vui lòng thử lại");
            }
            return Ok("Update thành công");
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy ="delete_customer_only")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            //var result = await _customerRepon.Delete(customer);
            if (result == 1)
            {
                return BadRequest("Không tìm thấy Customer");
            }
            else if (result == 3)
            {
                return BadRequest("Đã xảy ra lỗi, vui lòng thử lại");
            }
            return Ok("Xóa thành công");
        }

        [HttpGet]
        [Route("GetById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetById(string Id)
        {
            var authToken = HttpContext.Request.Headers["Authorization"];
            //var result = await _customerRepon.GetById(Guid.Parse(Id));
            var result = await _mediator.Send(new GetCustomerByIdQuery(Guid.Parse(Id)));
            if (result == null) return Ok("Không tìm thấy khách hàng");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetByPhoneNumber/{phonenumber}")]
        [Authorize]
        public async Task<IActionResult> GetByPhoneNumber(string phonenumber)
        {
            var result = await _mediator.Send(new GetCustomerByPhoneNumberQuery(phonenumber));
            if (result == null) return Ok("Không tìm thấy");
            return Ok(result);
        }
    }
}
