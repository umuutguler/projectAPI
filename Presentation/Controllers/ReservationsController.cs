using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
        public async Task<IActionResult> AllReservationsByUserIdAsync([FromQuery] ReservationParameters reservationParameters)
        {
            var userId = HttpContext.User.Identity.Name;
            var pagedResult = await _manager.ReservationInfoService.GetAllReservationInfosByUserId(reservationParameters, false, userId);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            var response = new
            {
                Reservations = pagedResult.reservations,
                MetaData = pagedResult.metaData
            };

            return Ok(response);
        }

        




        [HttpGet("{id:ObjectId}")]
        public async Task<IActionResult> OneRReservationAsync([FromRoute] ObjectId id)
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
        [HttpPost("with-payment")]
        public async Task<IActionResult> ReservationWithPayment([FromBody] PaymentDto paymentDto)
        {
            //UserIdFromToken
            var userId = HttpContext.User.Identity.Name;

            var reservation = await _manager.ReservationInfoService.CreateReservationWithPayment(paymentDto, userId);
            return Ok(reservation);
        }

        [HttpPut("{id:ObjectId}")]
        public async Task<IActionResult> OneReservationAsync([FromRoute(Name = "id")] ObjectId id,[FromBody] ReservationInfo reservationInfo)
        {

            //UserIdFromToken
            var userId = HttpContext.User.Identity.Name;

            await _manager.ReservationInfoService.UpdateOneReservationInfoAsync(id, reservationInfo, false, userId);
            var updatedReservation = await _manager.ReservationInfoService.GetOneReservationInfoByIdAsync(id, false);

            return StatusCode(200, updatedReservation);
        }

        [HttpDelete("{id:ObjectId}")]
        public async Task<IActionResult> OneReservationAsync([FromRoute(Name = "id")] ObjectId id)
        {

            await _manager.ReservationInfoService.DeleteOneReservationInfoAsync(id, false);

            return NoContent();
        }
        [HttpPut("cancel/{id:ObjectId}")]
        public async Task<IActionResult> CancelReservation([FromRoute(Name = "id")] ObjectId id)
        {
            var userId = HttpContext.User.Identity.Name;
            await _manager.ReservationInfoService.CancelOneReservationInfoAsync(id, false, userId);
            return NoContent();
        }

        [HttpGet("Empty")]
        public async Task<IActionResult> AllEmptyChairsAsync([FromQuery] ReservationInfo reservation)
        {
            //UserIdFromToken
            var userId = HttpContext.User.Identity.Name;
            return Ok(await _manager
                .ChairService.GetAllEmptyChairsAsync(reservation ,false, userId));
        }


        [HttpPut("uptodate")]
        public async Task<IActionResult> ReservationsUpToDate()
        {
            await _manager.ReservationInfoService.AreReservationsUpToDate();
            return NoContent();
        }

        [HttpGet("statistics/by-time-range")]
        public async Task<IActionResult> ReservationsStatistics([FromBody] ReservationDateRange dateRange)
        {
            var reservationsStatistics = await _manager.ReservationsStatisticsService.GetReservationsByTimeRange(dateRange.ReservationStartDate, dateRange.ReservationEndDate, false);
            return Ok(reservationsStatistics);
        }

        [HttpGet("statistics/mostReserved")]
        public async Task<IActionResult> MostReservedDepartment([FromBody] ReservationDateRange dateRange)
        {
            var mostReservedDepartment = await _manager.ReservationsStatisticsService.MostReservedDepartmentAsync(dateRange.ReservationStartDate, dateRange.ReservationEndDate, false);
            return Ok(mostReservedDepartment);
        }

        [HttpGet("statistics/mostReservedUser")]
        public async Task<IActionResult> MostReservedUser([FromBody] ReservationDateRange dateRange)
        {
            var mostReservedUser = await _manager.ReservationsStatisticsService.MostReservedUserAsync(dateRange.ReservationStartDate, dateRange.ReservationEndDate, false);
            return Ok(mostReservedUser);
        }


        [HttpGet("statistics/mostCancelledUser")]
        public async Task<IActionResult> MostCancelledUser([FromBody] ReservationDateRange dateRange)
        {
            var mostCancelledUser = await _manager.ReservationsStatisticsService.MostCancelledUserAsync(dateRange.ReservationStartDate, dateRange.ReservationEndDate, false);
            return Ok(mostCancelledUser);
        }

        [HttpGet("TableReservationCount/{id}")]
        public async Task<IActionResult> TableReservationCount([FromRoute] int id, [FromBody] ReservationDateRange dateRange)
        {
            var ReservedTableCount = await _manager.ReservationsStatisticsService.GetReservedChairCountByTableIdAsync(id, dateRange.ReservationStartDate, dateRange.ReservationEndDate, false);
            return Ok(ReservedTableCount);
        }

        [HttpGet("statistics/chair-occupancy-rate")]
        public async Task<IActionResult> ChairOccupancyRate([FromBody] ReservationDateRange dateRange)
        {
            var chairOccupancyRate = await _manager.ReservationsStatisticsService.ChairOccupancyRate(dateRange.ReservationStartDate, dateRange.ReservationEndDate, false);
            return Ok(chairOccupancyRate);
        }

        [HttpGet("statistics/Report")]
        public async Task<IActionResult> GenerateReservationReport([FromRoute] int id, [FromBody] ReservationDateRange dateRange)
        {
            var report = await _manager.ReservationsStatisticsService.GenerateReservationReport(id, dateRange.ReservationStartDate, dateRange.ReservationEndDate, false);
            return Ok(report);
        }



        public class ReservationDateRange
        {
            public DateTime ReservationStartDate { get; set; }
            public DateTime ReservationEndDate { get; set; }
        }


    }
}
