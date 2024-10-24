using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Domain.Entities
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int Number { get; set; }
        public string Row { get; set; }
        public bool IsReserved { get; set; }

        public int SlotId { get; set; } 
        public Slot Slot { get; set; } 

        public ICollection<BookingSeat> BookingSeats { get; set; }
    }
}
