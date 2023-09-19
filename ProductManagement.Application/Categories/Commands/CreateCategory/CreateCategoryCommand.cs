using MediatR;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Common.ValidationResults;

namespace ProductManagement.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Result>
{
    public CategoryDto Category { get; set; }
}