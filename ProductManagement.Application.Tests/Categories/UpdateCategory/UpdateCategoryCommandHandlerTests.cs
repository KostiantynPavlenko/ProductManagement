using FluentAssertions;
using Moq;
using ProductManagement.Application.Categories.Commands.CreateCategory;
using ProductManagement.Application.Categories.Commands.UpdateCategory;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Domain.Categories;

namespace ProductManagement.Application.Tests.Categories.UpdateCategory;

public class UpdateCategoryCommandHandlerTests
{
    private readonly Mock<IRepository<Category>> _categoryRepository;
    public UpdateCategoryCommandHandlerTests()
    {
        _categoryRepository = new Mock<IRepository<Category>>();
    }

    [Fact]
    public async void UpdateCategoryHandle_WithCorrectInput_CategoryIsUpdated()
    {
        var command = new UpdateCategoryCommand
        {
            Category = new CategoryDto
            {
                Name = "TestCategory"
            }
        };

        _categoryRepository.Setup(x => x.Update(It.IsAny<Category>()))
            .Returns(Task.FromResult(true))
            .Verifiable();
        
        var updateCommandHandler = new UpdateCategoryCommandHandler(_categoryRepository.Object);

        var result = await updateCommandHandler.Handle(command, new CancellationToken());
        
        _categoryRepository.Verify(x => x.Update(It.IsAny<Category>()), Times.Exactly(1));
        
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async void UpdateCategoryHandle_WithInvalidInput_CategoryIsNotUpdated()
    {
        var command = new UpdateCategoryCommand
        {
            Category = new CategoryDto
            {
                Name = "TestCategory"
            }
        };

        _categoryRepository.Setup(x => x.Update(It.IsAny<Category>()))
            .Returns(Task.FromResult(false))
            .Verifiable();
        
        var updateCommandHandler = new UpdateCategoryCommandHandler(_categoryRepository.Object);

        var result = await updateCommandHandler.Handle(command, new CancellationToken());
        
        _categoryRepository.Verify(x => x.Update(It.IsAny<Category>()), Times.Exactly(1));
        
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Categories.UpdateCategory);
    }
}