using ShopProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Products
{
    public record ProductDTO(int Id , string Name ,string Description ,decimal Price , string CategoryName , int Stock);
    
}
