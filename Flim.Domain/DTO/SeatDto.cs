using Flim.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Domain.DTO
{
    public class SeatDto
    {
        public int seatCount { get; set; }

        public int filmId { get; set; }

        public string Row { get; set; }

        public DateOnly SlotDate { get; set; }

        public ShowEnum category { get; set; }

        


    }
}
