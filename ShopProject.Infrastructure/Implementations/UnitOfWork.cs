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

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Customers = new GenericRepository<Customer>(context);
        }

        public async Task<int> CompleteAsync() => await context.SaveChangesAsync();

        public void Dispose() => context.Dispose();


    }
}
