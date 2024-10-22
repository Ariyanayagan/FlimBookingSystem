using Flim.Application.DTOs;
using Flim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Interfaces
{
    public interface IShowtimeService
    {
        Task<Showtime> CreateShowtimeAsync(ShowTimeDTO showtime);
        Task<IEnumerable<Showtime>> GetShowtimesByFilmAsync(int filmId);
    }
}
