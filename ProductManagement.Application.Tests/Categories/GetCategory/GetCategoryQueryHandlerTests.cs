using AutoMapper;
using Extensions.Web.Results;
using FluentAssertions;
using Moq;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Categories.Queries.GetCategory;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Domain.Categories;

namespace ProductManagement.Application.Tests.Categories.GetCategory;

public class GetCategoryQueryHandlerTests
{
    private readonly Mock<IRepository<Category>> _categoryRepository;
    private readonly Mock<IMapper> _mapper;
    

    public GetCategoryQueryHandlerTests()
    {
        _categoryRepository = new Mock<IRepository<Category>>();
        _mapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task Handle_ValidCategoryId_ReturnsCategoryDtoResult()
    {
        var categoryId = Guid.NewGuid();
        var category = new Category
        {
            Name = "Test"
        };

        var categoryDto = new CategoryDto
        {
            Name = "Test"
        };

        _mapper.Setup(x => x.Map<Category, CategoryDto>(category))
            .Returns(categoryDto)
            .Verifiable();

        _categoryRepository.Setup(x => x.GetById(categoryId))
            .ReturnsAsync(category)
            .Verifiable();
        
        var query = new GetCategoryQuery
        {
            CategoryId = categoryId
        };
        
        var handler = new GetCategoryQueryHandler(_categoryRepository.Object, _mapper.Object);
        var result = await handler.Handle(query, new CancellationToken());

        _mapper.Verify(x => x.Map<Category, CategoryDto>(category), Times.Exactly(1));
        _categoryRepository.Verify(x => x.GetById(categoryId), Times.Exactly(1));
        
        result.Should().BeAssignableTo<Result<CategoryDto>?>();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(categoryDto);
    }
    
    [Fact]
    public async Task Handle_WithInvalidCategoryId_ReturnsNothing()
    {
        var categoryId = Guid.NewGuid();
        var categoryDto = new CategoryDto
        {
            Name = "Test"
        };

        _mapper.Setup(x => x.Map<Category, CategoryDto>(default!))
            .Returns(default(CategoryDto)!)
            .Verifiable();

        _categoryRepository.Setup(x => x.GetById(categoryId))
            .ReturnsAsync((Category) null!);
        
        var query = new GetCategoryQuery
        {
            CategoryId = categoryId
        };
        
        var handler = new GetCategoryQueryHandler(_categoryRepository.Object, _mapper.Object);
        var result = await handler.Handle(query, new CancellationToken());

        _mapper.Verify(x => x.Map<Category, CategoryDto>(default!),Times.Never);
        _categoryRepository.Verify(x => x.GetById(categoryId), Times.Exactly(1));
        
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Categories.GetCategory);
    }
}