using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderAPI.Handlers.OrderHandlers;
using OrderAPI.Migrations;
using OrderAPI.Models;
using OrderAPI.Repositories.IRepon;
using OrderAPI.Services.IService;
using OrderAPI.ViewModel.CustomerViewModel;
using OrderAPI.ViewModel.OrderViewModel;
using System.Text;
using System.Text.Json;
using static OrderAPI.Commands.OrderCommands.OrderCommand;
using static OrderAPI.Queries.OrderQueries.OrderQuery;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepon _orderRepon;
        private readonly IOrderService _orderService;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private readonly IMediator _mediator;

        public OrderController(IOrderRepon orderRepon, HttpClient httpClient, IOrderService orderService, IMediator mediator)
        {
            _orderRepon = orderRepon;
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _orderService = orderService;
            _mediator = mediator;
            
        }

        [HttpGet]
        [Route("FindCustomer/{phoneNumber}")]
        [Authorize]
        public async Task<IActionResult> FindCustomer(string phoneNumber)
        {
            //var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7055/api/Customers/GetByPhoneNumber/" + phonenumber);

            //var response = await _httpClient.SendAsync(request);

            //if (!response.IsSuccessStatusCode)
            //{
            //    return BadRequest();
            //}

            //var json = await response.Content.ReadAsStringAsync();
            //return Ok(json);
            var response = await _orderService.FindCustomerByPhoneNumber(phoneNumber);
            if (response == "Không tìm thấy") return Ok(response);


            var customer = JsonConvert.DeserializeObject<ViewModelCustomer>(response);
            return Ok(customer);
        }

        [HttpPost]
        [Route("CreateOrder")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateOrder order)
        {
            //var result = await _orderRepon.Create(create);
            var result = await _mediator.Send(new CreateOrderCommand(order));
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateCustomer")]
        [Authorize]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomer create)
        {
            //var url = "https://localhost:7055/api/Customers/CreateCustomer/";

            //var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(create), Encoding.UTF8, "application/json");
            //var response = await _httpClient.PostAsync(url, content);

            //if (response.IsSuccessStatusCode)
            //{
            //    var responseString = await response.Content.ReadAsStringAsync();
            //    return Ok(responseString);
            //}
            //else
            //{
            //    return StatusCode((int)response.StatusCode);
            //}
            //var respone = await _orderService.AddCustomer(create);
            var response = await _mediator.Send(new CreateCustomerCommand(create));
            if (response == "Không tìm thấy") return Ok(response);
            var customer = JsonConvert.DeserializeObject<ViewModelCustomer>(response);
            return Ok(customer);

        }

        [HttpGet]
        [Route("GetByIdOrder/{Id}")]
        [Authorize]
        public async Task<IActionResult> Get(string Id)
        {
            //var order = await _mediator.Send(new GetByIdKhachHangQuery(Guid.Parse(Id)));
            var order = await _mediator.Send(new GetByIdOrderQuery(Guid.Parse(Id)));
            if (order != null)
            {
                var response = await _orderService.GetCustomer(order.CustomerId.ToString());
                //var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7055/api/Customers/GetById" + order.OrderObj.CustomerId.ToString());
                //var response = await _httpClient.SendAsync(request);  OrderObj.CustomerId.ToString()

                //if (!response.IsSuccessStatusCode)
                //{
                //    return BadRequest();
                //}

                //var json = await response.Content.ReadAsStringAsync();

                if (response == "Không tìm thấy khách hàng")
                {
                    var responses = new
                    {
                        KhachHang = response,
                        HoaDon = order,
                    };

                    return Ok(responses);
                }
                else
                {
                    var customer = JsonConvert.DeserializeObject<ViewModelCustomer>(response);
                    var responses = new
                    {
                        KhachHang = customer,
                        HoaDon = order,
                    };

                    return Ok(responses);
                }
            }
            return Ok("Không tìm thấy hóa đơn");
        }

        [HttpGet]
        [Route("GetByIdKhachHang/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdKhachHang(string Id)
        {
            var order = await _mediator.Send(new GetByIdKhachHangQuery(Guid.Parse(Id)));
            return Ok(order);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //var order = await _orderRepon.GetAll();
            var order = await _mediator.Send(new GetAllListOrderQuery());
            return Ok(order);
        }
    }
}
