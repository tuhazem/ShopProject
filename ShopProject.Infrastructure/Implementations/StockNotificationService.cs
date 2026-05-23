using Microsoft.AspNetCore.SignalR;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Infrastructure.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Infrastructure.Implementations
{
    public class StockNotificationService : IStockNotificationService
    {
        private readonly IHubContext<StockHub> _hubContext;

        public StockNotificationService(IHubContext<StockHub> hubContext)
        {
            this._hubContext = hubContext;
        }
        public Task NotifyStockUpdateAsync(int productId, string productName, int newStock, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.All.SendAsync("ReceiveStockUpdate", new
            {
                ProductId = productId,
                ProductName = productName,
                NewStock = newStock
            }, cancellationToken);
        }
    }
}
