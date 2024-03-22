using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        public async Task<IActionResult> AllReservationsAsync()
        {
            return Ok(await _manager
                .ReservationInfoService
                .GetAllReservationInfosAsync(false));
        }

        [HttpGet("User")]
        public async Task<IActionResult> AllReservationsByUserId()
        {
            var userId = HttpContext.User.Identity.Name;
            var reservations = await _manager
                .ReservationInfoService
                .GetAllReservationInfosByUserId(false, userId);
            return Ok(reservations);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> OneRReservationAsync([FromRoute] int id)
        {
            return Ok(await _manager.ReservationInfoService.GetOneReservationInfoByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> OneReservationAsync([FromBody] ReservationInfo reservationInfo)
        {
            //UserIdFromToken
            var userId = HttpContext.User.Identity.Name;

            var reservation = await _manager.ReservationInfoService.CreateOneReservationInfoAsync(reservationInfo, userId);
            return StatusCode(201, reservation);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> OneReservationAsync([FromRoute(Name = "id")] int id,[FromBody] ReservationInfo reservationInfo)
        {

            //UserIdFromToken
            var userId = HttpContext.User.Identity.Name;

            await _manager.ReservationInfoService.UpdateOneReservationInfoAsync(id, reservationInfo, false, userId);
            var updatedReservation = await _manager.ReservationInfoService.GetOneReservationInfoByIdAsync(id, false);

            return StatusCode(200, updatedReservation);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> OneReservationAsync([FromRoute(Name = "id")] int id)
        {

            await _manager.ReservationInfoService.DeleteOneReservationInfoAsync(id, false);

            return NoContent();
        }
        [HttpPut("cancel/{id:int}")]
        public async Task<IActionResult> CancelReservation([FromRoute(Name = "id")] int id)
        {
            var userId = HttpContext.User.Identity.Name;
            await _manager.ReservationInfoService.CancelOneReservationInfoAsync(id, false, userId);
            return NoContent();
        }

        [HttpGet("Empty")]
        public async Task<IActionResult> AllEmptyChairsAsync()
        {
            //UserIdFromToken
            var userId = HttpContext.User.Identity.Name;
            return Ok(await _manager
                .ChairService.GetAllEmptyChairsAsync(false, userId));
        }

        [HttpPut("uptodate")]
        public async Task<IActionResult> ReservationsUpToDate()
        {
            await _manager.ReservationInfoService.AreReservationsUpToDate();
            return NoContent();
        }

        [HttpGet("statistics/by-time-range")]
        public async Task<IActionResult> ReservationsStatistics([FromBody] DateTime startDate, DateTime endDate)
        {
            var reservationsStatistics = await _manager.ReservationsStatisticsService.GetReservationsStatisticsAsync(startDate, endDate, false);
            return Ok(reservationsStatistics);
        }

    }
}
