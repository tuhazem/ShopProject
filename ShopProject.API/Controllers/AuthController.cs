using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Common.Models;

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authService.RegisterAsync(model);
            if (result.IsAuthenticated)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await authService.GetTokenAsync(model);
            if (result.IsAuthenticated)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
}
    }
