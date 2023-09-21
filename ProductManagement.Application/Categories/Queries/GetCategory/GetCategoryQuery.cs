using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Categories.DTO;

namespace ProductManagement.Application.Categories.Queries.GetCategory;

public class GetCategoryQuery : IRequest<Result<CategoryDto>>
{
    public Guid CategoryId { get; set; }
}