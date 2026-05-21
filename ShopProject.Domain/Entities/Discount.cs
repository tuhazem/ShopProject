using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Entities
{
    public class Discount
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal Precentage { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Active { get; set; }
    }
}
