using AutoMapper;
using Extensions.Web.Results;
using FluentAssertions;
using Moq;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Categories.Queries.GetAllCategories;
using ProductManagement.Application.Categories.Queries.GetCategory;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Domain.Categories;

namespace ProductManagement.Application.Tests.Categories.GetCategories;

public class GetCategoriesQueryHandlerTests
{
    private readonly Mock<IRepository<Category>> _categoryRepository;
    private readonly Mock<IMapper> _mapper;
    

    public GetCategoriesQueryHandlerTests()
    {
        _categoryRepository = new Mock<IRepository<Category>>();
        _mapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task Handle_DataIsPersisted_ReturnsCategoryDtoList()
    {
        var category = new Category
        {
            Name = "Test"
        };

        var categoriesList = new List<Category>() {category};
        
        var categoryDto = new CategoryDto
        {
            Name = "Test"
        };
        
        var categoriesDtoList = new List<CategoryDto>() {categoryDto};
        
        _mapper.Setup(x => x.Map<List<Category>, List<CategoryDto>>(categoriesList))
            .Returns(categoriesDtoList)
            .Verifiable();

        _categoryRepository.Setup(x => x.GetAll())
            .ReturnsAsync(categoriesList)
            .Verifiable();

        var query = new GetAllCategoriesQuery();
        var handler = new GetAllCategoriesQueryHandler(_categoryRepository.Object, _mapper.Object);
        
        var result = await handler.Handle(query, new CancellationToken());

        _mapper.Verify(x => x.Map<List<Category>, List<CategoryDto>>(categoriesList), Times.Exactly(1));
        _categoryRepository.Verify(x => x.GetAll(), Times.Exactly(1));
        
        result.Should().BeAssignableTo<Result<List<CategoryDto>?>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeSameAs(categoriesDtoList);
    }
}