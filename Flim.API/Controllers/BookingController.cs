using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;

namespace Flim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableBookings([FromQuery] int id, [FromQuery] DateOnly date)
        {
            
            var results = await _bookingService.GetAvailableSeats(id,date);


            return Ok(results);

        }

        [HttpPost("hold")]
        public async Task<IActionResult> HoldTicketAsync([FromBody] BookingDTO booking)
        {

            var results = await _bookingService.HoldAsync(booking);


            return Ok(results);

        }


    }
}
