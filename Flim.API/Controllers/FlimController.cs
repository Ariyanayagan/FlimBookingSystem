using Flim.API.Common;
using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Flim.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Flim.API.Controllers
{
    [Route("api/film")]
    [ApiController]
    public class FlimController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FlimController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateFilm([FromBody] AddFilmDTO filmDto)
        {
            if (filmDto == null)
            {
                return BadRequest(ApiResponse<string>.Failure("Should Not Be an Empty", (int)HttpStatusCode.BadRequest));
            }

            var flim = await _filmService.CreateFilmAsync(filmDto);

            return Ok(ApiResponse<int>.Success(flim,statusCode:(int)HttpStatusCode.Created));    

        }

        [HttpGet("flim-name")]
        public async Task<IActionResult> GetFlimByName([FromQuery] string name)
        {
            var flims = await _filmService.GetFilmByNameAsync(name);
            return Ok(ApiResponse<IEnumerable<Film>>.Success(flims,statusCode:(int)HttpStatusCode.OK));
        }
        [HttpGet("film-id/{id}")]
        public async Task<IActionResult> GetFlimById([FromRoute] int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);

            if(film is null)
            {
                return BadRequest(ApiResponse<string>.Failure($"Flim Not Found for this Id : {id}", statusCode: (int)HttpStatusCode.NotFound));
            }

            var result = new FilmDTO
            {
                Name = film.Name,
                Description = film.Description,
                Duration = film.Duration,
                Genre = film.Genre
            };

            return Ok(ApiResponse<FilmDTO>.Success(result, statusCode: (int)HttpStatusCode.OK));
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync()
        {
            var films = await  _filmService.GetFilmAsync();


            var filmDtos = films.Select(f => new FilmDTO
            {
                Description = f.Description,
                Duration = f.Duration,
                FilmId = f.FilmId,
                Genre = f.Genre,
                Name = f.Name,
                Slots = f.Slots.Select(s => new Slots
                {
                    ShowCategory = s.ShowCategory,
                    FilmId = s.FilmId,
                    SlotDate = s.SlotDate,
                    SlotId = s.SlotId,
                    Seats = s.Seats.Select(st => new SeatsDTO
                    {
                        SlotId = st.SlotId,
                        IsReserved = st.IsReserved,
                        Number = st.Number,
                        Row = st.Row,
                        SeatId = st.SeatId
                    }).ToList()


                }).ToList()

            });

            return Ok(filmDtos);

            
            
        }

    }
}
