using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Domain.Entities
{
    public class BookingSeat
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public int SeatId { get; set; }
        public Seat Seat { get; set; }
    }
}
