using Flim.API.Common;
using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Flim.Domain.Entities;


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

        [HttpGet("film")]
        public async Task<IActionResult> GetFlimByName([FromQuery] string name)
        {
            var flims = await _filmService.GetFilmByNameAsync(name);
            return Ok(ApiResponse<IEnumerable<Film>>.Success(flims,statusCode:(int)HttpStatusCode.OK));
        }
        [HttpGet("film/{id}")]
        public async Task<IActionResult> GetFlimById([FromRoute] int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);

            if(film is null)
            {
                return BadRequest(ApiResponse<string>.Failure($"Flim Not Found for this Id : {id}", statusCode: (int)HttpStatusCode.NotFound));
            }
            return Ok(ApiResponse<FilmDTO>.Success(film, statusCode: (int)HttpStatusCode.OK));
        }

    }
}
