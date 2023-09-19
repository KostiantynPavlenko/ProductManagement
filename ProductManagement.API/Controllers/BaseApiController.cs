using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Common.ValidationResults;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
     protected ActionResult HandleResult<T>(Result<T> result)
     {
         switch (result.StatusCode)
         {
             case HttpStatusCode.OK:
                 return Ok(result.Value);
             case HttpStatusCode.NotFound:
                 return NotFound(result.Error);
             case HttpStatusCode.BadRequest:
                 return BadRequest(result.Error);
             case HttpStatusCode.Unauthorized:
                 return Unauthorized(result.Error);
             default:
             {
                 var response = new ObjectResult(result.Error)
                 {
                     StatusCode = StatusCodes.Status500InternalServerError
                 };

                 return response;
             }
         }
     }
     
     protected ActionResult HandleResult(Result result)
     {
         switch (result.StatusCode)
         {
             case HttpStatusCode.OK:
                 return Ok();
             case HttpStatusCode.NotFound:
                 return NotFound(result.Error);
             case HttpStatusCode.BadRequest:
                 return BadRequest(result.Error);
             case HttpStatusCode.Unauthorized:
                 return Unauthorized(result.Error);
             default:
             {
                 var response = new ObjectResult(result.Error)
                 {
                     StatusCode = StatusCodes.Status500InternalServerError
                 };

                 return response;
             }
         }
     }
}