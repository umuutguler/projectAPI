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
    }
}
