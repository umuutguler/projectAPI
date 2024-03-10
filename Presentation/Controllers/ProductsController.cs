using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Repositories.Contracts;
using Services.Contracts;


namespace Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))] // En üste eklediğimizde bütün metodların loglanmasını sağlayabiliriz.
                                                // Action bazlı değil de controller bazlı loglama işlemi yapıyoruz

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
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _manager.ProductService.GetAllProductsAsync(false);
            return Ok(products);

        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneProductAsync([FromRoute(Name = "id")] int id)
        {

            var product = await _manager.ProductService.GetOneProductByIdAsync(id, false);

            return Ok(product);

        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]  // ServiceFilter yapısı
        [HttpPost]
        public async Task<IActionResult> CreateOneProductAsync([FromBody] ProductDtoForInsertion productDto)
        {
            var product = await _manager.ProductService.CreateOneProductAsync(productDto);
            // _manager.Save(); // Manager üzerinden save işlemi
            return StatusCode(201, product); // CreatedAtRoute()
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]  // ServiceFilter yapısı
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UptadeOneProductAsync([FromRoute(Name = "id")] int id,
         [FromBody] ProductDtoForUpdate productDto)
        {
            await _manager.ProductService.UpdateOneProductAsync(id, productDto, false);

            return NoContent(); //204
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneProductAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.ProductService.DeleteOneProductAsync(id, false);

            return NoContent();
        }


        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUptadeOneProductAsync([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<ProductDtoForUpdate> productPatch)
        {
            if (productPatch is null)
                return BadRequest();

            var result = await _manager.ProductService.GetOneProductForPatchAsync(id, false);

            productPatch.ApplyTo(result.productDtoForUpdate, ModelState);

            TryValidateModel(result.productDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.ProductService.SaveChangesForPatchAsync(result.productDtoForUpdate, result.product);

            return NoContent(); // 204
        }
    }
}
