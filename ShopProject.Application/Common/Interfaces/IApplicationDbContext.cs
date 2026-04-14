using Microsoft.EntityFrameworkCore;
using ShopProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
            DbSet<Product> Products { get;  }
            DbSet<Category> Categories { get; }
            Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
