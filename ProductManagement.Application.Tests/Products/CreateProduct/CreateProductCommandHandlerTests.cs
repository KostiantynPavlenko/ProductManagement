using FluentAssertions;
using Moq;
using ProductManagement.Application.Categories.Commands.CreateCategory;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Products.Commands.CreateProduct;
using ProductManagement.Application.Tests.Common;
using ProductManagement.Domain.Categories;
using ProductManagement.Domain.Products;

namespace ProductManagement.Application.Tests.Products.CreateProduct;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IRepository<Product>> _repository;
    public CreateProductCommandHandlerTests()
    {
        _repository = new Mock<IRepository<Product>>();
    }

    [Fact]
    public async void CreateHandle_CorrectDataIsProvided_ProductIsCreated()
    {
        var command = new CreateProductCommand()
        {
            Name = DataGenerator.GenerateString(100),
            CategoryId = Guid.NewGuid(),
            Price = 123,
            Sku = DataGenerator.GenerateString(50)
        };

        _repository.Setup(x => x.Create(It.IsAny<Product>()))
            .Returns(Task.FromResult(true))
            .Verifiable();
        
        var createCommandHandler = new CreateProductCommandHandler(_repository.Object);

        var result = await createCommandHandler.Handle(command, new CancellationToken());
        
        _repository.Verify(x => x.Create(It.IsAny<Product>()), Times.Exactly(1));
        
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async void CreateCategoryHandle_CorrectDataIsProvided_CategoryIsNotCreated()
    {
        var command = new CreateProductCommand()
        {
            Name = DataGenerator.GenerateString(100),
            CategoryId = Guid.NewGuid(),
            Price = 123,
            Sku = DataGenerator.GenerateString(50)
        };

        _repository.Setup(x => x.Create(It.IsAny<Product>()))
            .Returns(Task.FromResult(false))
            .Verifiable();
        
        var createCommandHandler = new CreateProductCommandHandler(_repository.Object);

        var result = await createCommandHandler.Handle(command, new CancellationToken());
        
        _repository.Verify(x => x.Create(It.IsAny<Product>()), Times.Exactly(1));

        result.Error.Should().Be(DomainErrors.Products.ProductCreationFailed);
        result.IsFailure.Should().BeTrue();
    }
}