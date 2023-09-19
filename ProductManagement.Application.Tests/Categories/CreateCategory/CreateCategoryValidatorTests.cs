using FluentAssertions;
using FluentValidation;
using ProductManagement.Application.Categories.Commands.CreateCategory;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Tests.Common;

namespace ProductManagement.Application.Tests.Categories.CreateCategory;

public class CreateCategoryValidatorTests
{
    private readonly CreateCategoryValidator _createCategoryValidator;
    
    public CreateCategoryValidatorTests()
    {
        _createCategoryValidator = new CreateCategoryValidator();
    }
    
    [Theory]
    [InlineData(99, 0)]
    [InlineData(100, 0)]
    [InlineData(101, 1)]
    public void Validate_PassCategoryNameInVariousLength_ValidationIsVerified(int categoryNameLength, int errorCount)
    {
        var createCategoryCommand = new CreateCategoryCommand
        {
            Category = new CategoryDto
            {
                Name = DataGenerator.GenerateString(categoryNameLength)
            }
        };

        var result = _createCategoryValidator.Validate(createCategoryCommand);
        
        result.Errors.Count.Should().Be(errorCount);
    }
}