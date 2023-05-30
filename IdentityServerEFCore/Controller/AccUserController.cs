using IdentityServerEFCore.Models;
using IdentityServerEFCore.Repositories.IRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerEFCore.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccUserController : ControllerBase
    {
        private readonly IAccountRepository _accUserRepon;
        public AccUserController(IAccountRepository accUserRepon)
        {
            _accUserRepon = accUserRepon;
        }

        [HttpPost]
        //[Route("Create")]
        //[Authorize]
        public async Task<IActionResult> CreateAccUser([FromBody] SignUpModel createAccUser)
        {
            var result = await _accUserRepon.SignUpAsync(createAccUser);
            if (result.Succeeded)
            {
                return Ok(1);
            }
            else { return Ok(result); }

        }
    }
}
