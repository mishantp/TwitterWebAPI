using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterWebAPI.Models;
using TwitterWebAPI.Service;

namespace TwitterWebAPI.Controllers
{
    [Authorize]
    [Route("api/tweets/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("username")]
        public async Task<IActionResult> Search(string username)
        {
            return Ok(await _userService.SearchUsersByNameAsync(username));
        }
    }
}
