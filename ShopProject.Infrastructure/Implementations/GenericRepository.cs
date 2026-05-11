using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopProject.Infrastructure.Persistence;
using System.Linq.Expressions;
using ShopProject.Domain.Entities;

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

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await context.Set<T>().ToListAsync();


        public async Task<T?> GetByIdAsync(int id) => await context.Set<T>().FindAsync(id);

        public IQueryable<T> GetQueryable()
        {
            return context.Set<T>().AsQueryable();
        }

        public void Update(T entity) => context.Set<T>().Update(entity);

        

    }
}
