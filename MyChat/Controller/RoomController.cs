using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyChat.Context;
using MyChat.Models;
using MyChat.Repositories;
using MyChat.ViewModel;
using System.Xml.Linq;

namespace MyChat.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepo _roomRepo;

        public RoomController(IRoomRepo roomRepo)
        {
            _roomRepo = roomRepo;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] Rooms model)
        {
            var result = await _roomRepo.CreateRoom(model);
            if (result == null) return BadRequest("Đã xảy ra lỗi, vui lòng thử lại");
            return Ok(result);
        }
    }
}
