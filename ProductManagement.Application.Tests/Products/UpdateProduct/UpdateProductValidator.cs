using FluentAssertions;
using FluentValidation;
using ProductManagement.Application.Products.Commands.UpdateProduct;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Application.Tests.Common;

namespace ProductManagement.Application.Tests.Products.UpdateProduct;

public class UpdateProductValidatorTests
{
    private readonly UpdateProductValidator _updateProductValidator;
    
    public UpdateProductValidatorTests()
    {
        _updateProductValidator = new UpdateProductValidator();
    }
    
    [Fact]
    public void Validate_WithValidProductData_ValidationIsPassed()
    {
        var updateProductCommand = new UpdateProductCommand()
        {
            Product = new ProductDto
            {
                Name = DataGenerator.GenerateString(20),
                CategoryId = Guid.NewGuid(),
                Price = 123,
                ProductId = Guid.NewGuid(),
                Sku = DataGenerator.GenerateString(20)
            }
        };

        var result = _updateProductValidator.Validate(updateProductCommand);

        result.IsValid.Should().BeTrue();
        result.Errors.Count.Should().Be(0);
    }
    
    [Fact]
    public void Validate_WithInvalidProductData_ValidationIsPassed()
    {
        var updateProductCommand = new UpdateProductCommand()
        {
            Product = new ProductDto
            {
                Name = DataGenerator.GenerateString(1000),
                CategoryId = Guid.Empty,
                Price = default,
                ProductId = Guid.Empty,
                Sku = DataGenerator.GenerateString(1000)
            }
        };

        var result = _updateProductValidator.Validate(updateProductCommand);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Fact]
    public void Validate_WithInvalidSkuProductData_ValidationIsPassed()
    {
        var updateProductCommand = new UpdateProductCommand()
        {
            Product = new ProductDto
            {
                Name = DataGenerator.GenerateString(50),
                CategoryId = Guid.Empty,
                Price = default,
                ProductId = Guid.Empty,
                Sku = DataGenerator.GenerateString(10)
            }
        };

        var result = _updateProductValidator.Validate(updateProductCommand);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}