using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        public readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /*[HttpPost("make-payment")]
        public IActionResult MakePayment()
        {
            var paymentResult = _paymentService.MakePayment();
            // İşlem sonucuna göre uygun bir yanıt döndürün
            return Ok(paymentResult); // Örneğin, ödeme işlemi başarılı olduysa 200 OK yanıtı döndürün
        }*/

       /* [HttpGet("payment-status")]
        public IActionResult CheckPaymentStatus()
        {
            var paymentStatus = _paymentService.CheckPaymentStatus();

            if (paymentStatus == PaymentStatus.Successful)
            {
                return Ok("Payment is successful");
            }
            else
            {
                return NotFound("Payment not found or failed"); // Ödeme bulunamadı veya başarısız ise 404 Not Found yanıtı döndürülür
            }*/
        
    }
}
