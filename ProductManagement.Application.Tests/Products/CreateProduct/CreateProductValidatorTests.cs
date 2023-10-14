using FluentAssertions;
using ProductManagement.Application.Categories.Commands.CreateCategory;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Products.Commands.CreateProduct;
using ProductManagement.Application.Products.Commands.UpdateProduct;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Application.Tests.Common;

namespace ProductManagement.Application.Tests.Products.CreateProduct;

public class CreateProductValidatorTests
{
    private readonly CreateProductValidator _createProductValidator;
    
    public CreateProductValidatorTests()
    {
        _createProductValidator = new CreateProductValidator();
    }
    
    [Fact]
    public async Task Validate_WithValidProductData_ValidationIsPassed()
    {
        var command = new CreateProductCommand()
        {
            Name = DataGenerator.GenerateString(100),
            CategoryId = Guid.NewGuid(),
            Price = 123,
            Sku = DataGenerator.GenerateString(50)
        };

        var result = await _createProductValidator.ValidateAsync(command);

        result.IsValid.Should().BeTrue();
        result.Errors.Count.Should().Be(0);
    }
    
    [Fact]
    public async Task Validate_WithInvalidProductData_ValidationIsPassed()
    {
        var updateProductCommand = new CreateProductCommand()
        {
            Name = DataGenerator.GenerateString(1000),
            CategoryId = Guid.Empty,
            Price = default,
            Sku = DataGenerator.GenerateString(1000)
        };

        var result = await _createProductValidator.ValidateAsync(updateProductCommand);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Fact]
    public async Task Validate_WithInvalidSkuProductData_ValidationFails()
    {
        var updateProductCommand = new CreateProductCommand()
        {
            Name = DataGenerator.GenerateString(50),
            CategoryId = Guid.Empty,
            Price = default,
            Sku = DataGenerator.GenerateString(10)
        };

        var result = await _createProductValidator.ValidateAsync(updateProductCommand);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}