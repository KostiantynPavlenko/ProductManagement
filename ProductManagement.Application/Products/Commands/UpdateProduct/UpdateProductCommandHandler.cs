using AutoMapper;
using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Domain.Products;

namespace ProductManagement.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IRepository<Product> _productRepository;

    public UpdateProductCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Product.ProductId,
            request.Product.Name,
            request.Product.CategoryId, 
            request.Product.Sku, 
            request.Product.Price);

        var result = await _productRepository.Update(product);
        
        return result
            ? Result.Success()
            : Result.Failure(DomainErrors.Products.ProductUpdatingFailed);
    }
}