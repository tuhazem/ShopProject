using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Features.Dashboard;

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator mediator;

        public AdminController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("dashboard-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDashboardStatus() {

            var status = await mediator.Send(new GetDashboardStatsQuery());
            return Ok(status);
        }
    }
}
