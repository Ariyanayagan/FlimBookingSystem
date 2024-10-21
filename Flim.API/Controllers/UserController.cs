using Flim.API.Common;
using Flim.API.Model;
using Flim.Infrastructures.Interfaces;
using Flim.Infrastructures.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Flim.Domain.Entities;
using BCrypt.Net;
using Flim.Application.Interfaces;

namespace Flim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginModel userLogin)
        {
           
            var user = await _userService.AuthenticateAsync(userLogin.email, userLogin.password);

            if(user is null)
            {
                return BadRequest(ApiResponse<string>.Failure("Invalid username or password",(int)HttpStatusCode.NotFound));
            }

            var token = _jwtService.GenerateJwtToken(user.UserId, user.Role);

            return Ok(ApiResponse<string>.Success(token,"You should use this token for every request"));

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var user = new User
            {
                Username = registerModel.Username,
                Email = registerModel.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerModel.Password,13),
                Role = "User",
            };



            var result = await _userService.RegisterAsync(user);

            if (!result)
            {
                return BadRequest(ApiResponse<string>.Failure("Registration failed", (int)HttpStatusCode.InternalServerError));
            }

            return Ok(ApiResponse<string>.Success("Register Successfully"));

        }
    }
}
