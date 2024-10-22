using Flim.API.Common;
using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Flim.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Flim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        public readonly IShowtimeService _showtimeService;

        public ShowController(IShowtimeService showtimeService)
        {
            _showtimeService = showtimeService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddShow([FromBody] ShowTimeDTO showTimeDTO)
        {
            var show = await _showtimeService.CreateShowtimeAsync(showTimeDTO);

            return Ok(ApiResponse<string>.Success(show.Film.Name, "Created successfully!", (int)HttpStatusCode.Created));
        }
    }
}
