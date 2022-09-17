using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TddExample.Application.Interface;
using TddExample.Application.Model;

namespace TddExample.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthBusiness _authBusiness;
        public AuthController(IAuthBusiness authBusiness)
        {
            _authBusiness = authBusiness;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _authBusiness.LoginAsync(model);

            if (result == null)
                return BadRequest(new { message = "Error message" });

            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Singin(RegisterModel model)
        {
            var result = await _authBusiness.RegisterAsync(model);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}
