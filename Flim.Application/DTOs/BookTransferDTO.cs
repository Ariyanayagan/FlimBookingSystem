using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.DTOs
{
    public class BookTransferDTO
    {
        public class FilmBookDTO
        {
            public int FilmId { get; set; }
            public string Description { get; set; }
            public string Genre { get; set; }
            public List<SlotDTO> Slots { get; set; }
        }

        public class SlotDTO
        {
            public int SlotId { get; set; }
            public List<SeatDTO> ShowCategory { get; set; }
        }

        public class SeatDTO
        {
            public int SeatId { get; set; }
            public string Row { get; set; }
            public int Number { get; set; }
        }

    }
}
