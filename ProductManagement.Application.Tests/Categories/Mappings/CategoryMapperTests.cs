using AutoMapper;
using FluentAssertions;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Categories.Mapping;
using ProductManagement.Domain.Categories;

namespace ProductManagement.Application.Tests.Categories.Mappings;

public class CategoryMapperTests
{
    private IMapper _mapper;

    public CategoryMapperTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CategoryMapper());
        });

        _mapper = configuration.CreateMapper();
    }
    
    [Fact]
    public void AutoMapper_MapSingleObject_ResultsAreEquivalent()
    {
        var category = new Category { Id = Guid.NewGuid(), Name = "Test Category" };

        var categoryDto = _mapper.Map<CategoryDto>(category);
        
        categoryDto.Should()
            .BeEquivalentTo(category, options => options
            .ExcludingMissingMembers()); 
    }
    
    [Fact]
    public void AutoMapper_MapObjectList_ResultsAreEquivalent()
    {
        var category = new Category { Id = Guid.NewGuid(), Name = "Test Category" };
        var categoryList = new List<Category>() {category};
        var result = _mapper.Map<List<CategoryDto>>(categoryList);
        
        result.Should()
            .SatisfyRespectively(first =>
        {
             first.Should().BeEquivalentTo(category, options => options
                 .ExcludingMissingMembers()); 
        })
            .And
            .ContainItemsAssignableTo<CategoryDto>();
    }
}