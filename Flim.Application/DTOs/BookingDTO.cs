using Flim.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.DTOs
{
    public class BookingDTO
    {
        public int FilmId { get; set; }

        public ShowEnum category { get; set; }

        public DateOnly date {  get; set; }

        public List<int> SeatNumbers { get; set; }

    }
}
