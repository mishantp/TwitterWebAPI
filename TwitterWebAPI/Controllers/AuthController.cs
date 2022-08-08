using Microsoft.AspNetCore.Mvc;
using TwitterWebAPI.Data;
using TwitterWebAPI.Dtos;
using TwitterWebAPI.Models;

namespace TwitterWebAPI.Controllers
{
    [Route("api/tweets")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserDto userDto)
        {
            var response = await _authRepository.RegisterAsync(
                new User
                {
                    LoginId = userDto.LoginId,
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    ContactNumber = userDto.ContactNumber
                }
                , userDto.Password);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var response = await _authRepository.LoginAsync(userLoginDto.LoginId, userLoginDto.Password);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("{username}/forgot")]
        public async Task<IActionResult> ForgotPassword(string username)
        {
            var response = await _authRepository.ForgotPasswordAsync(username);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
