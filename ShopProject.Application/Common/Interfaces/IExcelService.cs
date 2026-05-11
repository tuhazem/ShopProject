using ShopProject.Application.Features.Orders;
using ShopProject.Application.Features.Products;
using ShopProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Common.Interfaces
{
    public interface IExcelService
    {
        byte[] GenerateProductsReport(IEnumerable<ProdctShortDTO> products);
        byte[] GeneratePdfInvoice(Order order);
    }
}
