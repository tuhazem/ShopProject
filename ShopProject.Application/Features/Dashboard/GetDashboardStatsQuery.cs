using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.DTOs;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Dashboard
{
    public record GetDashboardStatsQuery : IRequest<DashboardStatsDto>;

    public class GetDashboardStatsHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
    {
        private readonly IUnitOfWork uow;

        public GetDashboardStatsHandler(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow;

            var todayorder = await uow.Orders.FindAsync(o=> o.OrderDate >= today);

            var status = new DashboardStatsDto();

            status.TotalSalaryToday = await uow.Orders.GetQueryable()
                .Where(o => o.OrderDate >= today)
                .SumAsync(o=> o.TotalPrice , cancellationToken);

            status.TotalOrderToday = await uow.Orders.GetQueryable()
                .CountAsync(o=> o.OrderDate >= today , cancellationToken);

            status.TopSellingProducts = await uow.Orderitems.GetQueryable()
                .GroupBy(oi => oi.Product.Name)
                .Select(g => new TopProductDto
                {
                    ProductName = g.Key,
                    TimesSold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TimesSold)
                .Take(5)
                .ToListAsync(cancellationToken);

            status.LowStockAlert = await uow.Products.GetQueryable()
                .Where(p=> p.Stock < 10)
                .Select(p=> new LowStockProductDto { 
                    ProductName = p.Name,
                    CurrentStock = p.Stock
                })
                .ToListAsync(cancellationToken);

            return status;

        }
    }


}
