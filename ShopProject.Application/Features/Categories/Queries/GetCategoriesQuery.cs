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
    public record GetCategoriesQuery : IRequest<List<CategoryDTO>>;

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDTO>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCategoriesQueryHandler(IApplicationDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<CategoryDTO>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await context.Categories.Include(p=>p.Products).ProjectTo<CategoryDTO>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
