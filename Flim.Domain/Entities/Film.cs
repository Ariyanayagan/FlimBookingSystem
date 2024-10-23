using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Domain.Entities
{
    public class Film
    {
        public int FilmId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; } // in minutes
        public string Genre { get; set; }

        public decimal Amount { get; set; } 

       //public ICollection<Showtime> Showtimes { get; set; }

        public ICollection<Slot> Slots { get; set; }
    }
}
