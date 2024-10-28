using Flim.Application.DTOs;
using Flim.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Interfaces
{
    /// <summary>
    /// Contract of seat service
    /// </summary>
    public interface ISeatService
    {
        Task AddSeat(SeatDto seatDto);
    }
}
