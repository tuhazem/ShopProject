using ShopProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Order> Orders { get; }

        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<OrderItem> Orderitems { get; }

        Task<int> CompleteAsync();
    }
}
