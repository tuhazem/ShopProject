using AutoMapper;
using ShopProject.Application.Features.Categories;
using ShopProject.Application.Features.Categories.Commands;
using ShopProject.Application.Features.Products;
using ShopProject.Application.Features.Products.Commands;
using ShopProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<UpdateProductCommand, Product>();
            CreateMap<UpdateCategoryCommand, Category>();


            CreateMap<Product, ProdctShortDTO>();
            CreateMap<Category, CategoryDTO>();
        }

    }
}
