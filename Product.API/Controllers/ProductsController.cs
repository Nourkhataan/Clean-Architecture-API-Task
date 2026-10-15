using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Application.DTOs;
using Products.Application.Features.Products.Commands;
using Products.Application.Features.Products.Queries;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetProductsQuery());

            return Ok(products);

        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProductCommand command)
        {
            var product = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetAll),
                new { id = product.Id },
                product);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _mediator.Send(
                new GetProductByIdQuery(id));

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateProductDto dto)
        {
            var command = new UpdateProductCommand(
                id,
                dto.Name,
                dto.Price);

            var product = await _mediator.Send(command);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _mediator.Send(
                new DeleteProductCommand(id));

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
