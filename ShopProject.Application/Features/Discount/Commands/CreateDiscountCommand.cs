using MediatR;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Discount.Commands
{
    public record CreateDiscountCommand(string Code, decimal Percentage, DateTime ExpiryDate) : IRequest<int>;


    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, int>
    {
        private readonly IUnitOfWork uow;

        public CreateDiscountCommandHandler(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public async Task<int> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = new Domain.Entities.Discount
            {
                Code = request.Code,
                Precentage = request.Percentage,
                ExpiryDate = request.ExpiryDate,
                Active = true
            };

            await uow.Discount.AddAsync(discount);
            await uow.CompleteAsync();

            return discount.Id;

        }
    }

}
