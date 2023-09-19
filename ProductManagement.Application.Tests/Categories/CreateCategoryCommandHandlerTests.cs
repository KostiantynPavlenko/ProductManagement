﻿using Moq;
using ProductManagement.Application.Categories.Commands.CreateCategory;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Common;
using ProductManagement.Domain.Categories;

namespace ProductManagement.Application.Tests.Categories;

public class CreateCategoryCommandHandlerTests
{
    private readonly Mock<IRepository<Category>> _categoryRepository;
    public CreateCategoryCommandHandlerTests()
    {
        _categoryRepository = new Mock<IRepository<Category>>();
    }

    [Fact]
    public async void CreateCategory_CorrectData_Success()
    {
        var command = new CreateCategoryCommand
        {
            Category = new CategoryDto
            {
                Name = "TestCategory"
            }
        };

        _categoryRepository.Setup(x => x.Create(It.IsAny<Category>()))
            .Returns(Task.FromResult(true))
            .Verifiable();
        
        var createCommandHandler = new CreateCategoryCommandHandler(_categoryRepository.Object);

        var result = await createCommandHandler.Handle(command, new CancellationToken());
        
        _categoryRepository.Verify(x => x.Create(It.IsAny<Category>()), Times.Exactly(1));
        
        Assert.False(result.IsFailure);
    }
}