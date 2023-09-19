using AutoMapper;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Domain.Categories;

namespace ProductManagement.Application.Categories.Mapping;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(x => x.CategoryId, source => source.MapFrom(p => p.Id));
        
        CreateMap<CategoryDto, Category>();
    }
}