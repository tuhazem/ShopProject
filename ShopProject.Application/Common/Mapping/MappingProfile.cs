using AutoMapper;
using ShopProject.Application.Features.Categories;
using ShopProject.Application.Features.Categories.Commands;
using ShopProject.Application.Features.Orders;
using ShopProject.Application.Features.Products;
using ShopProject.Application.Features.Products.Commands;
using ShopProject.Domain.Entities;

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

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<Product, ProductBarcodeDTO>();

        }

    }
}
