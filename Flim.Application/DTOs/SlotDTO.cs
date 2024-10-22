using Flim.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.DTOs
{
    public class SlotDTO
    {
        public int FilmId { get; set; }

        public ShowEnum ShowCategory { get; set; }

        public DateOnly SlotDate { get; set; }
    }
}
