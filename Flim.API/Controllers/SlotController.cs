using Flim.API.Common;
using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Flim.API.Controllers
{
    /// <summary>
    /// Admin has only access to create a slot for film
    /// </summary>
    [Route("api/slot")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SlotController : ControllerBase
    {
        public readonly ISlotService _slotService;
        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        /// <summary>
        /// You can add a slot for a film. you can create only 3 slots per film 
        /// i.e, Morning, Afternoon , Midnight.
        /// </summary>
        /// <param name="slotDTO"></param>
        /// <returns></returns>
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
