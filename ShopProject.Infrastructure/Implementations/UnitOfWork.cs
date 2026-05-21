using ShopProject.Application.Common.Interfaces;
using ShopProject.Domain.Entities;
using ShopProject.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Infrastructure.Implementations
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public IGenericRepository<Customer> Customers { get; private set; }
        public IGenericRepository<Order> Orders { get; private set; }
        public IGenericRepository<Product> Products { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<OrderItem> Orderitems { get; private set; }
        public IGenericRepository<Discount> Discount { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Customers = new GenericRepository<Customer>(context);
            Orders = new GenericRepository<Order>(context);
            Products = new GenericRepository<Product>(context);
            Categories = new GenericRepository<Category>(context);
            Orderitems = new GenericRepository<OrderItem>(context);
            Discount = new GenericRepository<Discount>(context);
        }

        public async Task<int> CompleteAsync() => await context.SaveChangesAsync();

        public void Dispose() => context.Dispose();


    }
}
