using FluentAssertions;
using ProductManagement.Application.Categories.Commands.CreateCategory;
using ProductManagement.Application.Categories.Commands.UpdateCategory;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Tests.Common;

namespace ProductManagement.Application.Tests.Categories.UpdateCategory;

public class UpdateCategoryValidatorTests
{
    private readonly UpdateCategoryValidator _updateCategoryValidator;
    
    public UpdateCategoryValidatorTests()
    {
        _updateCategoryValidator = new UpdateCategoryValidator();
    }
    
    [Theory]
    [InlineData(99, 0, true)]
    [InlineData(100, 0,true)]
    [InlineData(101, 1,false)]
    public void Validate_PassCategoryNameInVariousLength_ValidationIsVerified(int categoryNameLength, int errorCount, bool isValid)
    {
        var updateCategoryCommand = new UpdateCategoryCommand
        {
            Category = new CategoryDto
            {
                Name = DataGenerator.GenerateString(categoryNameLength)
            }
        };

        var result = _updateCategoryValidator.Validate(updateCategoryCommand);

        result.IsValid.Should().Be(isValid);
        result.Errors.Count.Should().Be(errorCount);
    }
}