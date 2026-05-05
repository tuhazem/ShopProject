using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Common.DTOs
{
    public class DashboardStatsDto
    {
        public decimal TotalSalaryToday { get; set; }
        public int TotalOrderToday { get; set; }
        public List<TopProductDto> TopSellingProducts { get; set; }

        public List<LowStockProductDto> LowStockAlert { get; set; }
    }

    public class TopProductDto
    {
        public string ProductName { get; set; }
        public int TimesSold { get; set; }
    }

    public class LowStockProductDto
    {
        public string ProductName { get; set; }
        public int CurrentStock { get; set; }
    }
}
