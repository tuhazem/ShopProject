using MediatR;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Discount.Queries
{
    public record GetAllDiscountQuery : IRequest<IEnumerable<DiscountDto>>;

    public class GetAllDiscountQueryHandler : IRequestHandler<GetAllDiscountQuery, IEnumerable<DiscountDto>>
    {
        private readonly IUnitOfWork uow;

        public GetAllDiscountQueryHandler(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public async Task<IEnumerable<DiscountDto>> Handle(GetAllDiscountQuery request, CancellationToken cancellationToken)
        {
            var discounts = await uow.Discount.GetAllAsync();
            return discounts.Select(d => new DiscountDto
            {
                Code = d.Code,
                Percentage = d.Precentage,
                ExpiryDate = d.ExpiryDate,
                IsActive = d.Active
            });
        }
    }
}
