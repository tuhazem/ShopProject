using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Orders.Command
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int CustomerId { get; set; }
        public List<OrderItemRequest> Items { get; set; }

        
        public string? DiscountCode { get; set; }

    }

    public class OrderItemRequest() {

        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
