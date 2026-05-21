using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Common.Models;
using ShopProject.Application.Features.Products;
using ShopProject.Application.Features.Products.Commands;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ShopProject.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateProductCommand command)
        {

            var result = await mediator.Send(command);
            return Ok(result);
        }

        
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductDTO>>> GetAll([FromQuery] GetProductsWithPaginationQuery query)
        {
            return await mediator.Send(query);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            var result = await mediator.Send(new DeleteProductCommand(id));
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {

            var result = await mediator.Send(new GetProductByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> UpdateProductById(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
        
            var result = await mediator.Send(command);
            
            if (!result) {
                return NotFound();
            }
            return NoContent();
        }


    }
}
