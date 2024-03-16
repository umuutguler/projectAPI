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
        public async Task<IActionResult> GetAllChairsAsync()
        {
            return Ok(await _services
                .ChairService
                .GetAllChairsAsync(false));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GeOneChairsAsync([FromRoute] int id)
        {
            return Ok(await _services
                .ChairService
                .GetOneChairByIdAsync(id, false));
        }
    }
}
