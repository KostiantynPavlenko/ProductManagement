using MediatR;
using ProductManagement.Application.Common.ValidationResults;

namespace ProductManagement.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<Result>
{
    public Guid CategoryId { get; set; }
}