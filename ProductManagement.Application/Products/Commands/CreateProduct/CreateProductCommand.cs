using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Common.ValidationResults;

namespace ProductManagement.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Result>
{
    public string Name { get; set; }

    public string Sku { get; set; }

    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}