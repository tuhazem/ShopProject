using AutoMapper;
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
    public record GetProductByIdQuery(int id) : IRequest<ProductDTO>;

    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetProductByIdHandler(IApplicationDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await context.Products.Include(c=> c.Category).FirstOrDefaultAsync(p => p.Id == request.id , cancellationToken);
            return mapper.Map<ProductDTO>(product);
        }
    }


}
