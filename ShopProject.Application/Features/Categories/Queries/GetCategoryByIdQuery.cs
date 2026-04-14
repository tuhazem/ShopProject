using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Categories.Queries
{
    public record GetCategoryByIdQuery(int id) : IRequest<CategoryDTO>;

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery , CategoryDTO>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCategoryByIdQueryHandler(IApplicationDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CategoryDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.Include(p=>p.Products)
                .ProjectTo<CategoryDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p=> p.Id == request.id , cancellationToken);

            if (category == null)
            {
                throw new Exception("Category not found");
            }
            
            return category;
        }
    }


}
