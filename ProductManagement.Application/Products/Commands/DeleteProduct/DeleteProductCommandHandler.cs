using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Domain.Products;

namespace ProductManagement.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IRepository<Product> _productRepository;

    public DeleteProductCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.Delete(request.ProductId);
        return result
            ? Result.Success()
            : Result.Failure(DomainErrors.Products.ProductDeletion);
    }
}