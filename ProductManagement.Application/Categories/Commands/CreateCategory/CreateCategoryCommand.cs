using Extensions.Web.Results;
using MediatR;
using ProductManagement.Application.Categories.DTO;

namespace ProductManagement.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Result>
{
    public CategoryDto Category { get; set; }
}