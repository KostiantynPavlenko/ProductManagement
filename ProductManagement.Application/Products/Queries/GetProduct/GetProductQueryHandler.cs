using AutoMapper;
using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Domain.Products;

namespace ProductManagement.Application.Products.Queries.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<ProductDto>>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.ProductId);
        var productDto = _mapper.Map<Product, ProductDto>(product);
        return Result<ProductDto>.Success(productDto);
    }
}