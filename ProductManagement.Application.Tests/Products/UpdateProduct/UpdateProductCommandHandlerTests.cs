using AutoMapper;
using Extensions.Web.Results;
using FluentAssertions;
using Moq;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Products.Commands.UpdateProduct;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Application.Tests.Common;
using ProductManagement.Domain.Products;

namespace ProductManagement.Application.Tests.Products.UpdateProduct;

public class UpdateProductCommandHandlerTests
{
    private readonly Mock<IRepository<Product>> _repository;
    
    public UpdateProductCommandHandlerTests()
    {
        _repository = new Mock<IRepository<Product>>();
    }

    [Fact]
    public async Task Handle_WithValidData_ProductIsUpdated()
    {
        var productDto = new ProductDto
        {
            Name = DataGenerator.GenerateString(20),
            CategoryId = Guid.NewGuid(),
            Price = 123,
            ProductId = Guid.NewGuid(),
            Sku = DataGenerator.GenerateString(20)
        };

        var product = Product.Create(productDto.ProductId,
            productDto.Name,
            productDto.CategoryId,
            productDto.Sku,
            productDto.Price);
        
        _repository.Setup(x => x.Update(It.IsAny<Product>()))
            .Returns(Task.FromResult(true))
            .Verifiable();
        
        var updateProductCommand = new UpdateProductCommand
        {
            Product = productDto
        };

        var updateProductCommandHandler = new UpdateProductCommandHandler(_repository.Object);
        var result = await updateProductCommandHandler.Handle(updateProductCommand, CancellationToken.None);

        _repository.Verify(x => x.Update(It.IsAny<Product>()), Times.Exactly(1));

        result.Should().BeAssignableTo<Result>();
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task Handle_WithInvalidData_ProductIsNotUpdated()
    {
        var productDto = new ProductDto
        {
            Name = DataGenerator.GenerateString(20),
            CategoryId = Guid.NewGuid(),
            Price = 123,
            ProductId = Guid.NewGuid(),
            Sku = DataGenerator.GenerateString(20)
        };

        var product = Product.Create(productDto.ProductId,
            productDto.Name,
            productDto.CategoryId,
            productDto.Sku,
            productDto.Price);
        
        _repository.Setup(x => x.Update(It.IsAny<Product>()))
            .Returns(Task.FromResult(false))
            .Verifiable();
        
        var updateProductCommand = new UpdateProductCommand
        {
            Product = productDto
        };

        var updateProductCommandHandler = new UpdateProductCommandHandler(_repository.Object);
        var result = await updateProductCommandHandler.Handle(updateProductCommand, CancellationToken.None);

        result.Should().BeAssignableTo<Result>();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Products.ProductUpdatingFailed);
    }

}