using MediatR;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Discount.Queries
{
    public record GetDiscountByIdQuery(int id) : IRequest<DiscountDto>;

    public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, DiscountDto>
    {
        private readonly IUnitOfWork uow;

        public GetDiscountByIdQueryHandler(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public async Task<DiscountDto> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
        {
            var discount = await uow.Discount.GetByIdAsync(request.id);
            if (discount == null) return null;

            return new DiscountDto
            {
                Code = discount.Code,
                Percentage = discount.Precentage,
                ExpiryDate = discount.ExpiryDate,
                IsActive = discount.Active
            };

        }
    }
}
