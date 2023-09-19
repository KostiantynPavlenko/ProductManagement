using AutoMapper;
using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Domain.Products;

namespace ProductManagement.Application.Products.Queries.GetAllProducts;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, Result<List<ProductDto>>>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductQueryHandler(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAll();
        var productsDto = _mapper.Map<List<Product>, List<ProductDto>>(products);

        return Result<List<ProductDto>>.Success(productsDto);
    }
}