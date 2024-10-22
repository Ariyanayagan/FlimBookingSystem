using Flim.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Domain.Entities
{
    public class Slot
    {
        public int SlotId { get; set; }
        public DateOnly SlotDate { get; set; } 
        public ShowEnum ShowCategory { get; set; }

        public int FilmId { get; set; }

        public Film Film { get; set; } 

        public ICollection<Seat> Seats { get; set; } 
        public ICollection<Booking> Bookings { get; set; } 
    }
}
