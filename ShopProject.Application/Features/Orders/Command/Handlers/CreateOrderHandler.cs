using MediatR;
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

        public CreateOrderHandler(IUnitOfWork _uow)
        {
            uow = _uow;
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
            }

            order.TotalPrice = TotalPrice;

            await uow.Orders.AddAsync(order);
            await uow.CompleteAsync();

            return order.Id;
            


        }
    }
}
