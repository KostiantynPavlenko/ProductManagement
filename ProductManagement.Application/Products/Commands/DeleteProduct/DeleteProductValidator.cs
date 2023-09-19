using System.Data;
using FluentValidation;

namespace ProductManagement.Application.Products.Commands.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
    }
}