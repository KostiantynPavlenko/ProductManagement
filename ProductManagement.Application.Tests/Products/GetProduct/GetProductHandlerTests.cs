using AutoMapper;
using Extensions.Web.Results;
using FluentAssertions;
using Moq;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Application.Products.Queries.GetProduct;
using ProductManagement.Application.Tests.Common;
using ProductManagement.Domain.Products;

namespace ProductManagement.Application.Tests.Products.GetProduct;

public class GetProductHandlerTests
{
    private readonly Mock<IRepository<Product>> _repository;
    private readonly Mock<IMapper> _mapper;

    public GetProductHandlerTests()
    {
        _repository = new Mock<IRepository<Product>>();
        _mapper = new Mock<IMapper>();
    }
    
    [Fact]
    public async Task Handle_WithValidProductId_ReturnsProductDtoResult()
    {
        var productId = Guid.NewGuid();
        var product = Product.Create(Guid.NewGuid(),
            DataGenerator.GenerateString(20),
            Guid.NewGuid(),
            DataGenerator.GenerateString(20),
            111);
        
        var productDto = new ProductDto
        {
            ProductId = product.Id,
            Price = product.Price.Amount,
            CategoryId = product.CategoryId,
            Sku = product.Sku.Value,
            Name = product.Name
        };
        
        _repository.Setup(x => x.GetById(productId))
            .ReturnsAsync(product)
            .Verifiable();
        
        _mapper.Setup(x => x.Map<Product, ProductDto>(product))
            .Returns(productDto)
            .Verifiable();

        var query = new GetProductQuery()
        {
            ProductId = productId
        };
        
        var handler = new GetProductQueryHandler(_repository.Object, _mapper.Object);
        var result = await handler.Handle(query, new CancellationToken());

        _repository.Verify(x => x.GetById(productId),Times.Exactly(1));
        _mapper.Verify(x => x.Map<Product, ProductDto>(product), Times.Exactly(1));

        result.Should().BeAssignableTo<Result<ProductDto>?>();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(productDto);
    }
    
    [Fact]
    public async Task Handle_WithInvalidProductId_ReturnsNotFoundResult()
    {
        var productId = Guid.NewGuid();
        
        _repository.Setup(x => x.GetById(productId))
            .ReturnsAsync(default(Product)!)
            .Verifiable();
        
        _mapper.Setup(x => x.Map<Product, ProductDto>(default!))
            .Returns(default(ProductDto)!)
            .Verifiable();

        var query = new GetProductQuery()
        {
            ProductId = productId
        };
        
        var handler = new GetProductQueryHandler(_repository.Object, _mapper.Object);
        var result = await handler.Handle(query, new CancellationToken());

        _repository.Verify(x => x.GetById(productId),Times.Exactly(1));
        _mapper.Verify(x => x.Map<Product, ProductDto>(default!), Times.Exactly(1));

        result.Should().BeAssignableTo<Result<ProductDto>?>();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Products.NotFound);
    }
}