using Flim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.DTOs
{
    public class SeatsDTO
    {
        public int SeatId { get; set; }
        public int Number { get; set; }
        public string Row { get; set; }
        public bool IsReserved { get; set; }
        public int SlotId { get; set; }

        //public ICollection<BookingSeat> BookingSeats { get; set; }
    }
}
