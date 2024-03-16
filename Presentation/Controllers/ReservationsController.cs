using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/reservation")]
    public class ReservationsController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public ReservationsController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            return Ok(await _manager
                .ReservationInfoService
                .GetAllReservationInfosAsync(false));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneReservationAsync([FromBody] ReservationInfo reservationInfo)
        {
            var reservation = await _manager.ReservationInfoService.CreateOneReservationInfoAsync(reservationInfo);
            return StatusCode(201, reservation);
        }
    }
}
