using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Domain.Entities
{
    public class Showtime
    {
        public int ShowtimeId { get; set; }
        public DateTime StartTime { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }

        public ICollection<Seat> Seats { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
