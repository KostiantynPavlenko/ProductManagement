using FluentAssertions;
using Moq;
using ProductManagement.Application.Categories.Commands.DeleteCategory;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Domain.Categories;

namespace ProductManagement.Application.Tests.Categories.DeleteCategory;

public class DeleteCategoryCommandHandlerTests
{
    private readonly Mock<IRepository<Category>> _categoryRepository;
    public DeleteCategoryCommandHandlerTests()
    {
        _categoryRepository = new Mock<IRepository<Category>>();
    }

    [Fact]
    public async void DeleteCategoryHandle_CorrectCategoryIdIsProvided_CategoryIsDelete()
    {
        var categoryId = Guid.NewGuid();
        var command = new DeleteCategoryCommand()
        {
            CategoryId = categoryId
        };

        _categoryRepository.Setup(x => x.Delete(categoryId))
            .Returns(Task.FromResult(true))
            .Verifiable();
        
        var deleteCommandHandler = new DeleteCategoryCommandHandler(_categoryRepository.Object);

        var result = await deleteCommandHandler.Handle(command, new CancellationToken());
        
        _categoryRepository.Verify(x => x.Delete(categoryId), Times.Exactly(1));
        
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async void DeleteCategoryHandle_InvalidCategoryIdIsProvided_CategoryIsNotDeleted()
    {
        var command = new DeleteCategoryCommand()
        {
            CategoryId = default
        };

        _categoryRepository.Setup(x => x.Delete(default))
            .Returns(Task.FromResult(false))
            .Verifiable();
        
        var deleteCommandHandler = new DeleteCategoryCommandHandler(_categoryRepository.Object);

        var result = await deleteCommandHandler.Handle(command, new CancellationToken());
        
        _categoryRepository.Verify(x => x.Delete(default), Times.Exactly(1));

        result.Error.Should().Be(DomainErrors.Categories.DeleteCategoryFailed);
        result.IsFailure.Should().BeTrue();
    }
}