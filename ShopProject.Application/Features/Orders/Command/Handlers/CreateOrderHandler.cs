using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Orders.Command.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IUnitOfWork uow;
        private readonly IStockNotificationService notificationService;

        public CreateOrderHandler(IUnitOfWork _uow , IStockNotificationService notificationService )
        {
            uow = _uow;
            this.notificationService = notificationService;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {

                CustomerId = request.CustomerId,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            decimal TotalPrice = 0;
            var updatedProducts = new List<Product>();
            foreach (var itemRequest in request.Items) { 
            
                var product = await uow.Products.GetByIdAsync(itemRequest.ProductId);
                if (product == null || product.Stock < itemRequest.Quantity) {

                    throw new Exception($"{product.Name} is Out OF Stock");
                }

                var orderItems = new OrderItem
                {
                    ProductId = itemRequest.ProductId,
                    Quantity = itemRequest.Quantity,
                    UnitPrice = product.Price,
                };

                order.OrderItems.Add(orderItems);
                TotalPrice += (orderItems.Quantity * orderItems.UnitPrice);

                product.Stock -= itemRequest.Quantity;

                uow.Products.Update(product);
                updatedProducts.Add(product);

            }

            if(!string.IsNullOrEmpty(request.DiscountCode))
            {
                var discount = await uow.Discount.GetQueryable()
                    .FirstOrDefaultAsync(d => d.Code == request.DiscountCode && d.Active, cancellationToken);
                if (discount != null && discount.ExpiryDate >= DateTime.Now) { 
                    
                    order.DiscountId = discount.Id;
                    TotalPrice -= (TotalPrice * discount.Precentage);
                }
            }

            order.TotalPrice = TotalPrice;

            await uow.Orders.AddAsync(order);
            await uow.CompleteAsync();

            foreach (var item in updatedProducts )
            {
                await notificationService.NotifyStockUpdateAsync(item.Id, item.Name, item.Stock , cancellationToken);

            }

            return order.Id;
            


        }
    }
}
