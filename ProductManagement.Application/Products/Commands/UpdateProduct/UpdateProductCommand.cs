using MediatR;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Products.DTO;

namespace ProductManagement.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<Result>
{
    public ProductDto Product { get; set; }
}