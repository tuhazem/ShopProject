using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Products
{
    public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDTO>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

    }

    public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDTO>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetProductsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<ProductDTO>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = context.Products.Include(P=> P.Category).AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm)) {

                query = query.Where(p => p.Name.Contains(request.SearchTerm) || p.Description.Contains(request.SearchTerm));
            }

            if (request.MinPrice.HasValue) {

                query = query.Where(p => p.Price >= request.MinPrice.Value);
            }

            return await PaginatedList<ProductDTO>.CreatedAsync(
                query.ProjectTo<ProductDTO>(mapper.ConfigurationProvider), request.PageNumber, request.PageSize
                );
        }

    }}

