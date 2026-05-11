using MediatR;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Products
{
    public record GetAllProductQuery : IRequest<IEnumerable<ProdctShortDTO>>;

    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, IEnumerable<ProdctShortDTO>>
    {
        private readonly IUnitOfWork uow;

        public GetAllProductHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<ProdctShortDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await uow.Products.GetAllAsync();
            return products.Select(p => new ProdctShortDTO
            {
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                
            });
        }
    }



}
