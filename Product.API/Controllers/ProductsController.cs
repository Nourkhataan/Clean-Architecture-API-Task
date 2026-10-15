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
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all products.");

            var products = await _mediator.Send(new GetProductsQuery());


            return Ok(products);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {

            _logger.LogInformation("Creating a new product with name: {ProductName}",command.Name);

            var product = await _mediator.Send(command);

            _logger.LogInformation("Product created successfully with ID: {ProductId}", product.Id);

            return CreatedAtAction(
                nameof(GetAll),
                new { id = product.Id },
                product);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching product with ID: {ProductId}",id);

            var product = await _mediator.Send(new GetProductByIdQuery(id));

            if (product is null)
            {
                _logger.LogWarning("Product with ID: {ProductId} not found.", id);

                return NotFound();
            }

            _logger.LogInformation("Product with ID: {ProductId} retrieved successfully.", id);

            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdateProductDto dto)
        {
            _logger.LogInformation("Updating product with ID: {ProductId}",id);

            var command = new UpdateProductCommand(id,dto.Name,dto.Price);

            var product = await _mediator.Send(command);

            if (product is null)
            {
                _logger.LogWarning("Cannot update product. Product with ID: {ProductId} was not found.",id);

                return NotFound();
            }

            _logger.LogInformation("Product with ID: {ProductId} updated successfully.",id);

            return Ok(product);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting product with ID: {ProductId}", id);
            
            var deleted = await _mediator.Send(new DeleteProductCommand(id));

            if (!deleted)
            {
                _logger.LogWarning("Cannot delete product. Product with ID: {ProductId} was not found.",id);

                return NotFound();
            }

            _logger.LogInformation("Product with ID: {ProductId} deleted successfully.", id);

            return NoContent();
        }
     

    }
}
