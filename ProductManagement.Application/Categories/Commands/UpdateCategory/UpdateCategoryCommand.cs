using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Common.ValidationResults;

namespace ProductManagement.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Result>
{
    public CategoryDto Category { get; set; }
}