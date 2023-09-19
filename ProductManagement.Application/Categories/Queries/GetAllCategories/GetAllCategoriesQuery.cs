using MediatR;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Common.ValidationResults;

namespace ProductManagement.Application.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery() : IRequest<Result<List<CategoryDto>>>;