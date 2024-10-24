using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Records
{
    public record FilmRecord(
        int FilmId,
        string name,
        string Description,
        string Genre,
        int Duration,
        List<SlotRecord> Slots
    );

    public record ShowFilmRecord(
       string name,
       string Description,
       string Genre,
       int Duration,
       decimal amount
   );

    public record MyOrderRecord(
       string MovieName,
       decimal Amount,
       DateTime dateTIme,
       string ShowTime,
       int ShowTimeint,
       DateOnly SlotDate,
       List<int> seats 
   );
}
