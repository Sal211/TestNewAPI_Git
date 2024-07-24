using Microsoft.AspNetCore.Mvc;
using testNewAPI.DTOS;
using testNewAPI.Services.Contract;
using testNewAPI.ServicesResponse;
namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserLoginService _userService;
        public AuthenticationController(IUserLoginService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UsersDto user)
        {
            if (!ModelState.IsValid || user == null) return BadRequest(ModelState);
            var status = await  _userService.GetUserLoginAsync(user);
            if (!status.Success && status.Message.Contains("connection error.")) return StatusCode(500, "Database connection error.");
            if (!status.Success && status.Message == "Error") return StatusCode(500, $"Some thing went wrong in Service layer when Login");
            if (!status.Success && status.Message.Contains("Invalid Username or Password")) return Unauthorized("Invalid Username or Password!");
            return Ok(status);
        }
    }
}
