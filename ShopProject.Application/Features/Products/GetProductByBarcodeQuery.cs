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
    public record GetProductByBarcodeQuery(string Barcode) : IRequest<ProductBarcodeDTO>;

    public class GetProductByBarcodeHandler : IRequestHandler<GetProductByBarcodeQuery, ProductBarcodeDTO>
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public GetProductByBarcodeHandler(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<ProductBarcodeDTO> Handle(GetProductByBarcodeQuery request, CancellationToken cancellationToken)
        {
            var product = await uow.Products.GetQueryable()
                .FirstOrDefaultAsync(p => p.Barcode == request.Barcode, cancellationToken);



            if (product == null) throw new Exception("Product not found");

            if (product.Stock <= 0)
            {
                throw new Exception($"{product.Name} is Out of Stock!");
            }
            

            return mapper.Map<ProductBarcodeDTO>(product);
        }
    }


}
