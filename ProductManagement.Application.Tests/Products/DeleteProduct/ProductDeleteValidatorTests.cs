using FluentAssertions;
using ProductManagement.Application.Categories.Commands.DeleteCategory;
using ProductManagement.Application.Products.Commands.DeleteProduct;

namespace ProductManagement.Application.Tests.Products.DeleteProduct;

public class ProductDeleteValidatorTests
{
    private readonly DeleteProductValidator _deleteProductValidator;
    public ProductDeleteValidatorTests()
    {
        _deleteProductValidator = new DeleteProductValidator();
    }
    
    [Theory]
    [InlineData("c91a195d-2b3f-4f1c-8dd9-84211f5c5bc9", 0, true)]
    [InlineData("00000000-0000-0000-0000-000000000000", 1, false)]
    public async Task Validate_PassProductId_ProductIdIsVerified(Guid productId, int errorCount, bool isValid)
    {
        var createCategoryCommand = new DeleteProductCommand()
        {
            ProductId = productId
        };

        var result = await _deleteProductValidator.ValidateAsync(createCategoryCommand);

        result.IsValid.Should().Be(isValid);
        result.Errors.Count.Should().Be(errorCount);
    }
}