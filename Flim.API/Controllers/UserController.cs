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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Flim.Application.Records;

namespace Flim.API.Controllers
{
    /// <summary>
    /// User action like login , register and order history.
    /// </summary>
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

        /// <summary>
        /// User can login here
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns> Auth token will be return each login. </returns>
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

        /// <summary>
        /// New user register Here
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// You can track your booking here!
        /// </summary>
        /// <returns></returns>
        [HttpGet("my-bookings")]
        [Authorize]
        public async Task<IActionResult> GetBookingAsync()
        {
           

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _userService.GetBookingsAsync(Convert.ToInt32(userId));

            if (result is null)
            {
                return BadRequest(ApiResponse<string>.Failure("No Bookings", (int)HttpStatusCode.NotFound));
            }

            return Ok(ApiResponse<List<MyOrderRecord>>.Success(result:result));

        }
    }
}
