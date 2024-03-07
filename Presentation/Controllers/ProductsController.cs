using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;
using static Entities.Exceptions.NotFoundException;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ProductsController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _manager.ProductService.GetAllProducts(false);
            return Ok(products);

        }


        [HttpGet("{id:int}")]
        public IActionResult GetOneProduct([FromRoute(Name = "id")] int id)
        {

            var product = _manager.ProductService.GetOneProductById(id, false);

            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }
            return Ok(product);

        }


        [HttpPost]
        public IActionResult CreateOneProduct([FromBody] Product product)
        {
            if (product is null)
            {
                return BadRequest(); // 400
            }

            _manager.ProductService.CreateOneProduct(product);

            return StatusCode(201, product); // Created
        }


        [HttpPut("{id:int}")]
        public IActionResult UptadeOneProduct([FromRoute(Name = "id")] int id,
            [FromBody] Product product)
        {
            if (product is null)
                return BadRequest(); // 400

            _manager.ProductService.UpdateOneProduct(id, product, true);

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneProduct([FromRoute(Name = "id")] int id)
        {
            _manager.ProductService.DeleteOneProduct(id, false);

            return NoContent();
        }


        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUptadeOneProduct([FromRoute(Name = "id")] int id,
     [FromBody] JsonPatchDocument<Product> productPatch)
        {
            // check product
                var entity = _manager
                    .ProductService
                    .GetOneProductById(id, true);

            if (entity == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    message = $"Product with id:{id} could not found."
                }); // 404
            }
            productPatch.ApplyTo(entity);

            _manager.ProductService.UpdateOneProduct(id, entity, true); // ?
            return NoContent(); // 204
        }


    }
}
