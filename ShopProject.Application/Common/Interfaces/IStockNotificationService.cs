using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Common.Interfaces
{
    public interface IStockNotificationService
    {
        Task NotifyStockUpdateAsync(int productId, string productName, int newStock, CancellationToken cancellationToken);
    }
}
