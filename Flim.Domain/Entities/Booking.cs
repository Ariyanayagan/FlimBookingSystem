using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalCost { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }


        public int SlotId { get; set; }
        public Slot Slot { get; set; } 

        public ICollection<BookingSeat> BookingSeats { get; set; }
    }
}
