using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Features.Orders.Command;
using ShopProject.Domain.Entities;

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand createOrder) {

            try
            {
                var order = await mediator.Send(createOrder);
                return Ok(new { Message = "Order created successfully!", OrderId = order });

            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
    }
}
