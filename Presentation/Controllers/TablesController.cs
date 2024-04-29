using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/tables")]
    public class TablesController : ControllerBase
    {
        private readonly IServiceManager _services;

        public TablesController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> AllTablesAsync()
        {
            return Ok(await _services
                .TableService
                .GetAllTablesAsync(false));
        }

        [HttpGet("{id:ObjectId}")]
        public async Task<IActionResult> OneTablesAsync([FromRoute] ObjectId id)
        {
            return Ok(await _services
                .TableService
                .GetOneTableByIdAsync(id, false));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> TableByIdAsync([FromRoute(Name = "id")] ObjectId id, [FromBody] Table table)
        {
            var updatedTableEntitiy = await _services.TableService.UpdateTableByIdAsync(id, table, true);
            if (updatedTableEntitiy == null)
                return NotFound();

            return NoContent(); //204
        }

        [HttpDelete("{id:ObjectId}")]
        public async Task<IActionResult> TableByIdAsync(ObjectId id)
        {
            /*var deletedTable = await _services.TableService.DeleteTableByIdAsync(id, trackChanges: true);
            if (deletedTable == null)
                return NotFound();

            return NoContent(); // 204*/
            await _services.TableService.DeleteTableByIdAsync(id, trackChanges: true);
            return NoContent(); // 204
        }
    }
}
