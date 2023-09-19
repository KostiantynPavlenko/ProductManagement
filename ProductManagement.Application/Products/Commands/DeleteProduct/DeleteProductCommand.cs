using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Common.ValidationResults;

namespace ProductManagement.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<Result>
{
    public Guid ProductId { get; set; }
}