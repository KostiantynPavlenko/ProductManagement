using MediatR;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Common.ValidationResults;

namespace ProductManagement.Application.Categories.Queries.GetCategory;

public class GetCategoryQuery : IRequest<Result<CategoryDto>>
{
    public Guid CategoryId { get; set; }
}