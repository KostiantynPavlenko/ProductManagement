using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Categories.Commands.CreateCategory;
using ProductManagement.Application.Categories.Commands.DeleteCategory;
using ProductManagement.Application.Categories.Commands.UpdateCategory;
using ProductManagement.Application.Categories.DTO;
using ProductManagement.Application.Categories.Queries.GetAllCategories;
using ProductManagement.Application.Categories.Queries.GetCategory;
using ProductManagement.Application.Common.ValidationResults;

namespace ProductManagement.API.Controllers;

[Authorize]
public class CategoriesController : BaseApiController
{
    [Route("/categories")]
    [HttpGet]
    public async Task<ActionResult> GetAllCategories()
    {
        return HandleResult(await Mediator.Send(new GetAllCategoriesQuery()));
    }
    
    [Route("/categories/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetCategory([FromRoute]Guid id)
    {
        return HandleResult(await Mediator.Send(new GetCategoryQuery{CategoryId = id}));
    }
    
    [Route("/categories")]
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody]CategoryDto category)
    {
        return HandleResult(await Mediator.Send(new CreateCategoryCommand{Category = category}));
    }
    
    [Route("/categories")]
    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody]CategoryDto category)
    {
        return HandleResult(await Mediator.Send(new UpdateCategoryCommand{Category = category}));
    }
    
    [Route("/categories/{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory([FromRoute]Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteCategoryCommand{CategoryId = id}));
    }
}