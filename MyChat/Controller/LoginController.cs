using Microsoft.AspNetCore.Mvc;
using MyChat.Repositories;
using MyChat.ViewModel;

namespace MyChat.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public LoginController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _userRepo.Login(model);
            if (response == null) return BadRequest("Sai tài khoản hoặc mật khẩu");
            return Ok(response);
        }
        
    }
}
