using FluentValidation;

namespace ProductManagement.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Category.Name).NotEmpty().MaximumLength(100);
    }
}