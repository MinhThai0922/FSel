using APICustomer.ViewModel.UserViewModel;
using CustomerAPI.Repositories.IRepon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepon _userRepon;

        public LoginController(IUserRepon userRepo)
        {
            _userRepon = userRepo;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _userRepon.Login(model);
            if (response == null) return BadRequest("Sai tài khoản hoặc mật khẩu");
            return Ok(response);
        }
    }
}
