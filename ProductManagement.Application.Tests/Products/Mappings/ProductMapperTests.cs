using AutoMapper;
using FluentAssertions;
using Moq;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Categories.Mapping;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Application.Products.Mapping;
using ProductManagement.Application.Tests.Common;
using ProductManagement.Domain.Categories;
using ProductManagement.Domain.Products;

namespace ProductManagement.Application.Tests.Products.Mappings;

public class ProductMapperTests
{
    private IMapper _mapper;

    public ProductMapperTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProductMapper());
        });

        _mapper = configuration.CreateMapper();
    }
    
    [Fact]
    public void AutoMapper_MapSingleObject_ResultsAreEquivalent()
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
        
        var categoryDto = _mapper.Map<ProductDto>(product);
        
        categoryDto.Should()
            .BeEquivalentTo(product, options => options
            .ExcludingMissingMembers()); 
    }
    
    [Fact]
    public void AutoMapper_MapObjectList_ResultsAreEquivalent()
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
        
        var result = _mapper.Map<List<ProductDto>>(productList);
        
        result.Should()
            .SatisfyRespectively(first =>
        {
             first.Should().BeEquivalentTo(product, options => options
                 .ExcludingMissingMembers()); 
        })
            .And
            .ContainItemsAssignableTo<ProductDto>();
    }
}