using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Domain.Products;

namespace ProductManagement.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery() : IRequest<Result<List<ProductDto>>>;