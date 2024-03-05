using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly RepositoriesContext _context;

        public ProductsController(RepositoriesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _context.Products.ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneProduct([FromRoute(Name = "id")] int id)
        {
            try
            {
                var product = _context
                .Products
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();  // LINQ Sorgusu

                if (product is null)
                {
                    return NotFound(); //404
                }
                return Ok(product);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                _context.Products.Add(product);
                _context.SaveChanges();   
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
                // check book
                var entity = _context
                    .Products
                    .Where(b => b.Id.Equals(id))
                    .SingleOrDefault();

                if (entity is null)
                {
                    return NotFound(); // 404
                }

                if (id != product.Id)
                    return BadRequest(); // 400
                                          
                entity.Title = product.Title;
                entity.Price = product.Price;

                _context.SaveChanges();
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
                // check book
                var entity = _context
                    .Products
                    .Where(b => b.Id.Equals(id))
                    .SingleOrDefault();

                if (entity is null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = $"Product with id:{id} could not found."
                    }); // 404
                }

                _context.Products.Remove(entity);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneProduct([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Product> productPatch)
        {
            try
            {
                // check entity
                var entity = _context
                    .Products
                    .Where(b => b.Id.Equals(id))
                    .SingleOrDefault();

                if (entity is null)
                    return NotFound(); // 404

                productPatch.ApplyTo(entity);
                _context.SaveChanges();

                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
