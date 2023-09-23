using AutoMapper;
using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Domain.Categories;

namespace ProductManagement.Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Result<CategoryDto>>
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetById(request.CategoryId);

        if (result is null)
        {
            return Result<CategoryDto>.NotFound(DomainErrors.Categories.CategoryNotFound);
        }

        var categoryDto = _mapper.Map<Category, CategoryDto>(result);

        return Result<CategoryDto>.Success(categoryDto);
    }
}