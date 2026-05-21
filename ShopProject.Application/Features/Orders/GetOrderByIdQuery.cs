using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Orders
{
    public record GetOrderByIdQuery(int id) : IRequest<OrderDto>;

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IUnitOfWork uow;

        public GetOrderByIdQueryHandler(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await uow.Orders.GetQueryable()
                .Include(o => o.Discount)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == request.id , cancellationToken);

            return new OrderDto
            {
                Id = order.Id,
                CustomerName = (await uow.Customers.GetByIdAsync(order.CustomerId)).Name,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalPrice,
                DiscountCode = order.Discount != null ? order.Discount.Code : null,
                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }

    }
}
