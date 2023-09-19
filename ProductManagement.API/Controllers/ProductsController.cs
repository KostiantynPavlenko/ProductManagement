using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Products.Commands.CreateProduct;
using ProductManagement.Application.Products.Commands.DeleteProduct;
using ProductManagement.Application.Products.Commands.UpdateProduct;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Application.Products.Queries.GetAllProducts;
using ProductManagement.Application.Products.Queries.GetProduct;

namespace ProductManagement.API.Controllers;

[Authorize]
public class ProductsController : BaseApiController
{
   [HttpGet]
   [Route("/products")]
   public async Task<ActionResult> GetProducts()
   {
      return HandleResult(await Mediator.Send(new GetAllProductsQuery()));
   }
   
   [HttpPost]
   [Route("/products")]
   public async Task<ActionResult> CreateProduct([FromBody]CreateProductCommand createProductCommand)
   {
      return HandleResult(await Mediator.Send(createProductCommand));
   }
   
   [HttpPut]
   [Route("/products")]
   public async Task<ActionResult> UpdateProduct([FromBody]ProductDto product)
   {
      return HandleResult(await Mediator.Send(new UpdateProductCommand{Product = product}));
   }
   
   [HttpDelete]
   [Route("/products/{id}")]
   public async Task<ActionResult> DeleteProduct([FromRoute]Guid id)
   {
      return HandleResult(await Mediator.Send(new DeleteProductCommand{ProductId = id}));
   }
   
   [HttpGet]
   [Route("/products/{id}")]
   public async Task<ActionResult> GetProduct([FromRoute]Guid id)
   {
      return HandleResult(await Mediator.Send(new GetProductQuery(){ProductId = id}));
   }
}