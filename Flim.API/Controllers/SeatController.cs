using Flim.API.Common;
using Flim.Application.Interfaces;
using Flim.Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Flim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        public readonly ISeatService seatService;
        public SeatController(ISeatService seatService)
        {
            this.seatService = seatService;
        }

        [HttpPost("bulk-add")]
        public async Task<IActionResult> BulkAdd(SeatDto seat)
        {
            await seatService.AddSeat(seat);
            return Ok(ApiResponse<string>.Success("Added",statusCode:(int)HttpStatusCode.OK));
        }

    }
}
