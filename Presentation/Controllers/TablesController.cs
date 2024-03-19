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
    [Route("api/tables")]
    public class TablesController : ControllerBase
    {
        private readonly IServiceManager _services;

        public TablesController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTablesAsync()
        {
            return Ok(await _services
                .TableService
                .GetAllTablesAsync(false));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GeOneTablesAsync([FromRoute] int id)
        {
            return Ok(await _services
                .TableService
                .GetOneTableByIdAsync(id, false));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UptadeTableByIdAsync([FromRoute(Name = "id")] int id, [FromBody] Table table)
        {
            var updatedTableEntitiy = await _services.TableService.UpdateTableByIdAsync(id, table, true);
            if (updatedTableEntitiy == null)
                return NotFound();

            return NoContent(); //204
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTableByIdAsync(int id)
        {
            var deletedTable = await _services.TableService.DeleteTableByIdAsync(id, trackChanges: true);
            if (deletedTable == null)
                return NotFound();

            return NoContent(); // 204
        }
    }
}
