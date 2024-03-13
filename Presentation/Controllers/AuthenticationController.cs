using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthenticationController(IServiceManager service)
        {
            _service = service;
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))] // Dto nesnesinin boş olup olmasığını kontrol ediyordu (UserForRegistrationDto)
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto) // UserForRegistrationDto bilgisi request'in body'sinden gelecek -> [FromBody]
        {
            var result = await _service
                .AuthenticationManager
                .RegisterUser(userForRegistrationDto);

            if (!result.Succeeded) // Başarılı değil ise ilgili ifadeleri modele hata olarak 
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201); // kullanıcıya ait herhangi bir ifade olmayacak, sadece 201 koduyla dönüş yapacağız
        }
    }
}