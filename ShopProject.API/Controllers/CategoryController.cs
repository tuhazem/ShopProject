using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Features.Categories;
using ShopProject.Application.Features.Categories.Commands;
using ShopProject.Application.Features.Categories.Queries;

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCategory(CreateCategoryCommand command)
        {

            return await mediator.Send(command);

        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategories()
        {
            return await mediator.Send(new GetCategoriesQuery());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var result = await mediator.Send(new GetCategoryByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);


        }
    }
}
