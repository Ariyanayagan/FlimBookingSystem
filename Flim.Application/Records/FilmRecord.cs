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

    public record MyOrderRecord(
       string MovieName,
       decimal Amount,
       DateTime dateTIme,
       string ShowTime,
       DateOnly SlotDate
   );
}
