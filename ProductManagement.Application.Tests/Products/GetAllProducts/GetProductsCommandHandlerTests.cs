using AutoMapper;
using Extensions.Web.Results;
using FluentAssertions;
using Moq;
using ProductManagement.Application.Common;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Application.Products.Queries.GetAllProducts;
using ProductManagement.Application.Tests.Common;
using ProductManagement.Domain.Products;

namespace ProductManagement.Application.Tests.Products.GetAllProducts;

public class GetProductsCommandHandlerTests
{
    private readonly Mock<IRepository<Product>> _repository;
    private readonly Mock<IMapper> _mapper;


    public GetProductsCommandHandlerTests()
    {
        _repository = new Mock<IRepository<Product>>();
        _mapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task Handle_ProductsArePersisted_ListOfProductsIsReturned()
    {
        var product = Product.Create(Guid.NewGuid(),
            DataGenerator.GenerateString(20),
            Guid.NewGuid(),
            DataGenerator.GenerateString(20),
            111);

        var productList = new List<Product>() {product};

        var productDto = new ProductDto
        {
            ProductId = product.Id,
            Price = product.Price.Amount,
            CategoryId = product.CategoryId,
            Sku = product.Sku.Value,
            Name = product.Name
        };
        
        var productDtoList = new List<ProductDto>() {productDto};
        
        _repository.Setup(x => x.GetAll())
            .ReturnsAsync(productList)
            .Verifiable();
        
        _mapper.Setup(x => x.Map<List<Product>, List<ProductDto>>(productList))
            .Returns(productDtoList)
            .Verifiable();
        
        var getProductsQuery = new GetAllProductsQuery();
        var getProductsQueryHandler = new GetAllProductQueryHandler(_repository.Object, _mapper.Object);

        var result = await getProductsQueryHandler.Handle(getProductsQuery, CancellationToken.None);
        
        _repository.Verify(x => x.GetAll(), Times.Exactly(1));
        _mapper.Verify(x => x.Map<List<Product>, List<ProductDto>>(productList),Times.Exactly(1));
        
        result.Should().BeAssignableTo<Result<List<ProductDto>>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(1);
    }
}