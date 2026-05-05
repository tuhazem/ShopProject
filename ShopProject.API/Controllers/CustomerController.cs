using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Features.Customers.Commands;
using ShopProject.Application.Features.Customers.Queries;

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCustomerCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAll()
        {
            var result = await mediator.Send(new GetAllCustomersQuery());
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, UpdateCustomerCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id in URL does not match Id in request body.");
            var result = await mediator.Send(command);
            if (!result)
                return NotFound("Customer not found.");
            return Ok("Customer updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteCustomerCommand(id));
            if (!result)
                return NotFound("Customer not found.");
            return Ok("Customer deleted successfully.");
        }

        [HttpPost("{id:int}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            Console.WriteLine($"Restoring Customer with ID: {id}");
            var result = await mediator.Send(new RestoreCustomerCommand(id));
            if (!result)
                return NotFound($"Customer with ID {id} not found in DB or logic failed.");
            return Ok("Customer restored successfully.");
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetCustomer([FromQuery] int? id, [FromQuery] string? name) {

            var query = new GetCustomerByIdOrNameQuery { Id = id, Name = name };
            var result = await mediator.Send(query);

            if (result == null) return NotFound("This Customer Not Found");
            return Ok(result);
        }
    }
}