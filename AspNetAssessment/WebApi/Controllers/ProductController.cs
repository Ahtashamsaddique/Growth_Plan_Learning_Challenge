using Appplication.Features.Product.Commands;
using Appplication.Features.Product.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllPorductsQuery(), cancellationToken);
            if (result.Any())
                return Ok(result);
            else
                return NotFound("No Record Found");

        }
        [HttpPost("CreateProduct")]
        public async Task<IResult> CreateProduct(CreateProductCommand createProductCommand, CancellationToken cancellationToken)
        {
            if (createProductCommand.Name.IsNullOrEmpty())
            {
                return Results.BadRequest("Product Name is Required");
            }
            
            if (createProductCommand.Description.IsNullOrEmpty() )
            {
                return Results.BadRequest("Product Description is Required");
            }
            
            if (createProductCommand.Rate == 0)
            {
                return Results.BadRequest("Product Rate is Required");
            }
            
            var result = await _mediator.Send(createProductCommand, cancellationToken);
            if (result > 0)
                return Results.Ok("New Product Created with id="+result);
            else
                return Results.NotFound();
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand updateProductCommand, CancellationToken cancellationToken)
        {
            if (updateProductCommand.Id <= 0)
            {
                return BadRequest("Invalid Product Id");
            }
            if (updateProductCommand.Name.IsNullOrEmpty())
            {
                return BadRequest("Product Name is Required");
            }

            if (updateProductCommand.Description.IsNullOrEmpty())
            {
                return BadRequest("Product Description is Required");
            }

            if (updateProductCommand.Rate == 0)
            {
                return BadRequest("Product Rate is Required");
            }
            var result = await _mediator.Send(updateProductCommand, cancellationToken);
            if (result > 0)
                return Ok(result);
            else
                return NotFound("No Record Found");
        }
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommand deleteProductCommand, CancellationToken cancellationToken)
        {
            if (deleteProductCommand.Id <= 0)
            {
                return BadRequest("Invalid Product Id");
            }
            var result = await _mediator.Send(deleteProductCommand, cancellationToken);
            if (result > 0)
                return Ok(result);
            else
                return NotFound("No Record Found");
        }
    }
}
