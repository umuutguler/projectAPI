using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/userInfo")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public UserController(IServiceManager manager)
        {
            _manager = manager;
        }

        [Authorize]
        [HttpGet("allUsers")]
        public async Task<IActionResult> GetAllUsersAsync()=>
            Ok(await _manager.UserService.GetAllUsersAsync(false));

        [Authorize]
        [HttpGet("User")]
        public async Task<IActionResult> GetOneUserInfoLoggedIn()
        {
            var userId = HttpContext.User.Identity.Name;
            return Ok(await _manager.UserService.GetOneUserByIdAsync(userId, false));
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UptadeOneUserAsync([FromBody] User user)
        {
            var userId = HttpContext.User.Identity.Name;
            await _manager.UserService.UpdateOneUserAsync(userId, user, false);
            var updatedUserInfo = await _manager.UserService.GetOneUserByIdAsync(userId, false);

            return StatusCode(200,  updatedUserInfo);
        }

        [Authorize]
        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteOneUserAsync([FromBody] User user)
        {
            await _manager.UserService.DeleteOneUserAsync(user.Id, false);
            return NoContent();
        }


    }
}
