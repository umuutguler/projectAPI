using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IServiceManager _services;

        public DepartmentsController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> AllDepartmentsAsync()
        {
            return Ok(await _services
                .DepartmentService
                .GetAllDepartmentsAsync(false));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> OneDepartmentAsync([FromRoute] ObjectId id)
        {
            return Ok(await _services
                .DepartmentService
                .GetOneDepartmentByIdAsync(id, false));
        }
    }
}
