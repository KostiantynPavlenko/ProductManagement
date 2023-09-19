using FluentValidation;

namespace ProductManagement.Application.Categories.Commands.CreateCategory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Category.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}