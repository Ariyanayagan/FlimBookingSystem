using Flim.API.Common;
using Flim.API.Validators;
using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Flim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            if(id <= 0)
            {
                return BadRequest(ApiResponse<string>.Failure("Id should be greater than 0"));
            }


            var results = await _bookingService.GetAvailableSeats(id,date);

            if (results is null || results.Count == 0)
            {
                return NotFound(ApiResponse<string>.Failure(message: $"{id} not found in this date {date}", statusCode: (int)HttpStatusCode.NotFound));
            }


            return Ok(results);

        }

        [HttpPost("book-now")]
        public async Task<IActionResult> HoldTicketAsync([FromBody] BookingDTO booking)
        {

            await _bookingService.HoldAsync(booking);


            return Ok(ApiResponse<string>.Success(result:"You should make a payment with in 10 minutes for Confirmed!",statusCode:StatusCodes.Status200OK));

        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmTicketAsync([FromBody] ConfirmBookingDTO booking)
        {

            await _bookingService.ConfirmAsync(booking);


            return Ok(ApiResponse<string>.Success(result: "Your tickets are booked you can view your order history in my orders.",statusCode:StatusCodes.Status200OK));

        }


    }
}
