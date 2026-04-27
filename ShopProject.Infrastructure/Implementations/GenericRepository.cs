using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopProject.Infrastructure.Persistence;

namespace ShopProject.Infrastructure.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T entity) => await context.Set<T>().AddAsync(entity);


        public void Delete(T entity) => context.Set<T>().Remove(entity);


        public async Task<IEnumerable<T>> GetAllAsync() => await context.Set<T>().ToListAsync();


        public async Task<T?> GetByIdAsync(int id) => await context.Set<T>().FindAsync(id);


        public void Update(T entity) => context.Set<T>().Update(entity);

    }
}
