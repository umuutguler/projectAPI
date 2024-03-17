using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneRReservationAsync([FromRoute] int id)
        {
            return Ok(await _manager.ReservationInfoService.GetOneReservationInfoByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneReservationAsync([FromBody] ReservationInfo reservationInfo)
        {
            var reservation = await _manager.ReservationInfoService.CreateOneReservationInfoAsync(reservationInfo);
            return StatusCode(201, reservation);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UptadeOneReservationAsync([FromRoute(Name = "id")] int id,[FromBody] ReservationInfo reservationInfo)
        {
            await _manager.ReservationInfoService.UpdateOneReservationInfoAsync(id, reservationInfo, false);

            return NoContent(); //204
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneReservationAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.ReservationInfoService.DeleteOneReservationInfoAsync(id, false);

            return NoContent();
        }

        [HttpGet("Empty")]
        public async Task<IActionResult> GetAlEmptyChairsAsync()
        {
            return Ok(await _manager
                .ChairService.GetAllEmptyChairsAsync(false));
        }

    }
}
