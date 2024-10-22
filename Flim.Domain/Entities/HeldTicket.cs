using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Domain.Entities
{
    public class HeldTicket
    {
        public int HeldTicketId { get; set; }
        public int FilmId { get; set; }
        public int SlotId { get; set; }
        public int SeatId { get; set; }
        public DateTime HoldExpiration { get; set; }
        public int UserId { get; set; }

        public Film Film { get; set; }
        public Slot Slot { get; set; }
        public Seat Seat { get; set; }
        public User User { get; set; }
    }
}
