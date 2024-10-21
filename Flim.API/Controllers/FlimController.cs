using Flim.API.Common;
using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Flim.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class FlimController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FlimController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpPost("film")]
        public async Task<IActionResult> CreateFilm([FromBody] FilmDTO filmDto)
        {
            if (filmDto == null)
            {
                return BadRequest(ApiResponse<string>.Failure("Should Not Be an Empty", (int)HttpStatusCode.BadRequest));
            }

            var flim = await _filmService.CreateFilmAsync(filmDto);

            return Ok(ApiResponse<int>.Success(flim,statusCode:(int)HttpStatusCode.Created));    

        }

    }
}
