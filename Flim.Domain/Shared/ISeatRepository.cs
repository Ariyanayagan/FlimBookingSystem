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
    public interface ISeatRepository : IGenericRepository<Seat>
    {
        Task AddSeat(SeatDto seatDto);
    }
}
