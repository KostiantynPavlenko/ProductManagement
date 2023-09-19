using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Products.DTO;

namespace ProductManagement.Application.Products.Queries.GetProduct;

public class GetProductQuery : IRequest<Result<ProductDto>>
{
    public Guid ProductId { get; set; }
}