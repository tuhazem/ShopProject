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
            Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
