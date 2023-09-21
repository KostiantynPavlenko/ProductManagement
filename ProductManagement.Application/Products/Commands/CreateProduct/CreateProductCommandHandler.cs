﻿using AutoMapper;
using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Domain.Products;
using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
{
    private readonly IRepository<Product> _productRepository;

    public CreateProductCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Name,
            request.CategoryId, 
            request.Sku, 
            request.Price);

        var result = await _productRepository.Create(product);

        return result
            ? Result.Success()
            : Result.Failure(DomainErrors.Products.ProductCreation);
    }
}