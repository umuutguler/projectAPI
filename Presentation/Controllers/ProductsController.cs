using Entities.DataTransferObjects;
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
            var products = _manager.ProductService.GetAllProducts(false);
            return Ok(products);

        }


        [HttpGet("{id:int}")]
        public IActionResult GetOneProduct([FromRoute(Name = "id")] int id)
        {

            var product = _manager.ProductService.GetOneProductById(id, false);

            return Ok(product);

        }


        [HttpPost]
        public IActionResult CreateOneProduct([FromBody] ProductDtoForInsertion productDto)
        {
            if (productDto is null)
            {
                return BadRequest(); // 400
            }
            if (!ModelState.IsValid) // 422 Unprocessable Entity
            {
                return UnprocessableEntity(ModelState);
            }
            var product = _manager.ProductService.CreateOneProduct(productDto);
            // _manager.Save(); // Manager üzerinden save işlemi
            return StatusCode(201, product); // CreatedAtRoute()
        }


        [HttpPut("{id:int}")]
        public IActionResult UptadeOneProduct([FromRoute(Name = "id")] int id,
         [FromBody] ProductDtoForUpdate productDto)
        {
            if (productDto is null)
                return BadRequest(); // 400

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _manager.ProductService.UpdateOneProduct(id, productDto, false);

            return NoContent(); //204
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneProduct([FromRoute(Name = "id")] int id)
        {
            _manager.ProductService.DeleteOneProduct(id, false);

            return NoContent();
        }


        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUptadeOneProduct([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<ProductDto> productPatch)
        {
            // check product
            var productDto = _manager
                .ProductService
                .GetOneProductById(id, true);

            productPatch.ApplyTo(productDto);

            _manager.ProductService.UpdateOneProduct(id,
                 new ProductDtoForUpdate()
                 {
                     Id = productDto.Id,
                     Title = productDto.Title,
                     Price = productDto.Price,
                     Description = productDto.Description,
                     CreatedDate = productDto.CreatedDate,
                     LastUpdate = productDto.LastUpdate
                 },
                  true); //?
            return NoContent(); // 204
        }

    }
}
