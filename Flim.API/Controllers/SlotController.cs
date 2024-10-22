using Flim.API.Common;
using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Flim.API.Controllers
{
    [Route("api/slot")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        public readonly ISlotService _slotService;
        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateAsync([FromBody] SlotDTO slotDTO)
        {
            var result = await _slotService.CreateSlotAsync(slotDTO);

            if (!result)
            {
                return BadRequest(ApiResponse<bool>.Failure("Error Occured", (int)HttpStatusCode.BadRequest));
            }

            return Ok(ApiResponse<bool>.Success(result,statusCode: (int)HttpStatusCode.OK));

        }


    }
}
