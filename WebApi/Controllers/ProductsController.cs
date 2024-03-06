using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public ProductsController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _manager.Product.GetAllProducts(false);
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
                var product = _manager.Product.GetOneProductById(id, false);

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

                _manager.Product.CreateOneProduct(product);
                _manager.Save(); // Manager üzerinden save işlemi
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
                // check product
                var entity = _manager.Product.GetOneProductById(id, true); 

                if (entity == null)
                {
                    return NotFound(); // 404
                }

                if (id != product.Id)
                    return BadRequest(); // 400

                entity.Title = product.Title;
                entity.Price = product.Price;

                _manager.Save();
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
                // check product
                var entity = _manager.Product.GetOneProductById(id, false);

                if (entity == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = $"Product with id:{id} could not found."
                    }); // 404
                }

                _manager.Product.DeleteOneProduct(entity);
                _manager.Save();

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
                    .Product
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

                _manager.Product.Update(entity); // ?
                //_manager.Save();
                return NoContent(); // 204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}
