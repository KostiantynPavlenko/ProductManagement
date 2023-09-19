using FluentAssertions;
using ProductManagement.Application.Categories.Commands.DeleteCategory;

namespace ProductManagement.Application.Tests.Categories.DeleteCategory;

public class DeleteCategoryValidatorTests
{
    private readonly DeleteCategoryValidator _deleteCategoryValidator;
    public DeleteCategoryValidatorTests()
    {
        _deleteCategoryValidator = new DeleteCategoryValidator();
    }
    
    [Theory]
    [InlineData("c91a195d-2b3f-4f1c-8dd9-84211f5c5bc9", 0, true)]
    [InlineData("00000000-0000-0000-0000-000000000000", 1, false)]
    public void Validate_PassCategoryId_CategoryIdIsVerified(Guid categoryId, int errorCount, bool isValid)
    {
        var createCategoryCommand = new DeleteCategoryCommand
        {
            CategoryId = categoryId
        };

        var result = _deleteCategoryValidator.Validate(createCategoryCommand);

        result.IsValid.Should().Be(isValid);
        result.Errors.Count.Should().Be(errorCount);
    }
}