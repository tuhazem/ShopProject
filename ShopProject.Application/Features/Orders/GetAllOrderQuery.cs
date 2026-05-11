using MediatR;
using ShopProject.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Orders
{
    public record GetAllOrderQuery : IRequest<IEnumerable<OrderDto>>;

    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, IEnumerable<OrderDto>>
    {
        private readonly IUnitOfWork uow;
        public GetAllOrderQueryHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var query = uow.Orders.GetQueryable()
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product);

            return await query.Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerName = o.Customer.Name,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalPrice,
                Items = o.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            }).ToListAsync(cancellationToken);
        }
    }   

}
