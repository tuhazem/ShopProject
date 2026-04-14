using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Common.Models
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public PaginatedList(List<T> items , int count , int pageNumber , int pageSize )
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;

        }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public static async Task<PaginatedList<T>> CreatedAsync(IQueryable<T> source, int pageNumber, int PageSize)
        {

            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageNumber, PageSize);
        }

    }


}
