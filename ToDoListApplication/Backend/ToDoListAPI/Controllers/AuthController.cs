using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Dto;
using ToDoListAPI.Services.Interface;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<object?>> Register(UserDto request)
        {
            var user = await _authService.RegisterAsync(request);
            if (user == null)
                return BadRequest("User already exists.");
            //return Ok(user);
            return Ok(new { message = "User registered successfully", user = user });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var token = await _authService.LoginAsync(request);
            if (token == null)
                return BadRequest("Invalid username or password.");
            //return Ok(token);
            return Ok(new { token });
        }

        [HttpGet("AuthTest")]
        [Authorize]
        public IActionResult AuthTest()
        {
            return Ok("Test successful.");
        }

        [HttpGet("AuthAdminTest")]
        [Authorize(Roles = "Admin")]
        public IActionResult AuthAdminTest()
        {
            return Ok("Admin test successful.");
        }
    }
}
