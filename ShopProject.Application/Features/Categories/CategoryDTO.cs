using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Categories
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Products.ProdctShortDTO> Products { get; set; } = new List<Products.ProdctShortDTO>();
    }
}
