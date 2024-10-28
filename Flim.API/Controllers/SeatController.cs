using Flim.API.Common;
using Flim.Application.Interfaces;
using Flim.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Flim.API.Controllers
{
    /// <summary>
    /// Admin has only access to add seat for a film
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SeatController : ControllerBase
    {
        public readonly ISeatService seatService;
        public SeatController(ISeatService seatService)
        {
            this.seatService = seatService;
        }

        /// <summary>
        /// You can add seats for a film according to slot.
        /// </summary>
        /// <param name="seat"></param>
        /// <returns></returns>
        [HttpPost("bulk-add")]
        public async Task<IActionResult> BulkAdd([FromBody] SeatDto seat)
        {
            await seatService.AddSeat(seat);
            return Ok(ApiResponse<string>.Success("Added",statusCode:(int)HttpStatusCode.OK));
        }

    }
}
