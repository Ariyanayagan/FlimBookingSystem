using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.DTOs
{
    public class AddFilmDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; } // in minutes
        public string Genre { get; set; }
    }
}
