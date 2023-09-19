using AutoMapper;
using MediatR;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Domain.Categories;

namespace ProductManagement.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<CategoryDto>>>
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAll();
        var categoryDto = _mapper.Map<List<Category>, List<CategoryDto>>(category);

        return Result<List<CategoryDto>>.Success(categoryDto);
    }
}