﻿using Entities.DataTransferObjects;
using Entities.Models;
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
    [Route("api/chairs")]
    public class ChairController : ControllerBase
    {
        private readonly IServiceManager _services;

        public ChairController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> AllChairsAsync()
        {
            return Ok(await _services
                .ChairService
                .GetAllChairsAsync(false));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> OneChairsAsync([FromRoute] int id)
        {
            return Ok(await _services
                .ChairService
                .GetOneChairByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> OneChairAsync([FromBody] Chair chair)
        {
            var entity = await _services.ChairService.CreateOneReservationInfoAsync(chair);
            return StatusCode(201, entity);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> ChairByIdAsync([FromRoute(Name = "id")] int id, [FromBody] Chair updatedChair) 
        {
            var updatedChairEntitiy = await _services.ChairService.UpdateChairByIdAsync(id, updatedChair, true);
            if (updatedChairEntitiy == null)
                return NotFound();

            return NoContent(); //204
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ChairByIdAsync([FromRoute]int id)
        {
            await _services.ChairService.DeleteChairByIdAsync(id, trackChanges: true);
           

            return NoContent(); // 204
        }

    }
}
