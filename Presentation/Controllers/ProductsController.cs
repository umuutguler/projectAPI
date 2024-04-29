using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Repositories.Contracts;
using Services.Contracts;
using System.Text.Json;


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
        /*
        [HttpHead]
        [HttpGet(Name = "GetAllProductsAsync")]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] ProductParameters productParameters)
        //[FromQuery] -> BookParameters ifadesinin nasıl alınacağı. Bu ifadeler Query string ile gelecek
        // Query string -> /book?pageNumber=2&pageSize=10
        {
            var pagedResult = await _manager.ProductService.GetAllProductsAsync(productParameters, false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.products);
        }

        [Authorize]
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


        [HttpOptions]
        public IActionResult GetProductsOptions() // Asenkron yaptığımız bir şey olmadığı için IActionResult ifadesiyle dönüş sağlaycağız
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS"); //Response'un Header'ına ekle
                                                                                           // Allow key - Diğer HTTP metodları ise value
            return Ok();
        }
        */
    }
}
