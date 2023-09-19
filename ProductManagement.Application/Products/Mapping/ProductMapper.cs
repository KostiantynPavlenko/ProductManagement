using AutoMapper;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Domain.Products;

namespace ProductManagement.Application.Products.Mapping;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ProductId,
                source => source.MapFrom(p => p.Id))
            .ForMember(dest => dest.Name,
                source => source.MapFrom(p => p.Name))
            .ForMember(dest => dest.CategoryId,
                source => source.MapFrom(p => p.CategoryId))
            .ForMember(dest => dest.Price,
                source => source.MapFrom(p => p.Price.Amount))
            .ForMember(dest => dest.Sku,
                source => source.MapFrom(p => p.Sku.Value));
    }
}