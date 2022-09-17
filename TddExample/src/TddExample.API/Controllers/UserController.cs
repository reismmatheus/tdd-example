using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TddExample.Application.Interface;

namespace TddExample.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.Identity.GetUserId();
            var result = await _userBusiness.GetProfileAsync(userId);
            return Ok(result);
        }
    }
}
