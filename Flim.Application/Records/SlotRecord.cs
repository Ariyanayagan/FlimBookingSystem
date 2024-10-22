using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Records
{
    public record SlotRecord(
        int SlotId,
        string SlotName,
        List<SeatRecord> ShowCategory 
     );
}
