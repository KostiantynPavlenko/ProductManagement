using FluentValidation;

namespace ProductManagement.Application.Products.Commands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Product.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Product.Sku).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Product.CategoryId).NotEmpty();
        RuleFor(x => x.Product.Price).NotEmpty();
    }
}