using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.ValidationResults;
using ProductManagement.Domain.Categories;

namespace ProductManagement.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
{
    private readonly IRepository<Category> _categoryRepository;

    public UpdateCategoryCommandHandler(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = request.Category.CategoryId,
            Name = request.Category.Name
        };
        
        var result = await _categoryRepository.Update(category);

        return result ? Result.Success() : Result.Failure(DomainErrors.Categories.UpdateCategory);
    }
}