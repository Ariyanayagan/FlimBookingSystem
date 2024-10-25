using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Timestamp]
        public byte[] RowVersion { get; set; } // Concurrency token

        public ICollection<BookingSeat> BookingSeats { get; set; }
    }
}
