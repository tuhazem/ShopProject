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

namespace ShopProject.Application.Features.Products
{
    public record GetProductQuery: IRequest<List<ProductDTO>>;

    public class GetAllProductsQueryHandler : IRequestHandler<GetProductQuery, List<ProductDTO>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetAllProductsQueryHandler(IApplicationDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<ProductDTO>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await context.Products.Include(p=> p.Category).ProjectTo<ProductDTO>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

        }
    }
}

