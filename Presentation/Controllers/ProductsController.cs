using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

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
            try
            {
                var products = _manager.ProductService.GetAllProducts(false);
                return Ok(products);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult GetOneProduct([FromRoute(Name = "id")] int id)
        {

            try
            {
                var product = _manager.ProductService.GetOneProductById(id, false);

                if (product == null)
                {
                    return NotFound(); //404
                }
                return Ok(product);

            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpPost]
        public IActionResult CreateOneProduct([FromBody] Product product)
        {
            try
            {
                if (product is null)
                {
                    return BadRequest(); // 400
                }

                _manager.ProductService.CreateOneProduct(product);

                return StatusCode(201, product); // Created
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id:int}")]
        public IActionResult UptadeOneProduct([FromRoute(Name = "id")] int id,
            [FromBody] Product product)
        {
            try
            {
                if (product is null)
                    return BadRequest(); // 400

                _manager.ProductService.UpdateOneProduct(id, product, true);

                return Ok(product);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneProduct([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.ProductService.DeleteOneProduct(id, false);

                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUptadeOneProduct([FromRoute(Name = "id")] int id,
     [FromBody] JsonPatchDocument<Product> productPatch)
        {
            try
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}
