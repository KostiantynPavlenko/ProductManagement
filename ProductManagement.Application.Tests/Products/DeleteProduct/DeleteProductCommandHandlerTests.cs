using FluentAssertions;
using Moq;
using ProductManagement.Application.Categories.Commands.DeleteCategory;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Products.Commands.DeleteProduct;
using ProductManagement.Domain.Categories;
using ProductManagement.Domain.Products;

namespace ProductManagement.Application.Tests.Products.DeleteProduct;

public class DeleteProductCommandHandlerTests
{
    private readonly Mock<IRepository<Product>> _repository;
    
    public DeleteProductCommandHandlerTests()
    {
        _repository = new Mock<IRepository<Product>>();
    }

    [Fact]
    public async void DeleteProductHandle_CorrectProductIdIsProvided_ProductIsDeleted()
    {
        var productId = Guid.NewGuid();
        var command = new DeleteProductCommand()
        {
            ProductId = productId
        };

        _repository.Setup(x => x.Delete(productId))
            .Returns(Task.FromResult(true))
            .Verifiable();
        
        var deleteCommandHandler = new DeleteProductCommandHandler(_repository.Object);

        var result = await deleteCommandHandler.Handle(command, new CancellationToken());
        
        _repository.Verify(x => x.Delete(productId), Times.Exactly(1));
        
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async void DeleteCategoryHandle_InvalidCategoryIdIsProvided_CategoryIsNotDeleted()
    {
        var command = new DeleteProductCommand()
        {
            ProductId = default
        };

        _repository.Setup(x => x.Delete(default))
            .Returns(Task.FromResult(false))
            .Verifiable();
        
        var deleteCommandHandler = new DeleteProductCommandHandler(_repository.Object);

        var result = await deleteCommandHandler.Handle(command, new CancellationToken());
        
        _repository.Verify(x => x.Delete(default), Times.Exactly(1));

        result.Error.Should().Be(DomainErrors.Products.ProductDeletion);
        result.IsFailure.Should().BeTrue();
    }
}