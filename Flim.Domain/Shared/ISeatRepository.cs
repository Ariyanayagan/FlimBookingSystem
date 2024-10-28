using Flim.Domain.DTO;
using Flim.Domain.Entities;
using Flim.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Domain.Interfaces
{
    /// <summary>
    /// Extented Repostory to handle seat opearations.
    /// </summary>
    public interface ISeatRepository : IGenericRepository<Seat>
    {
        Task AddSeat(SeatDto seatDto);
    }
}
