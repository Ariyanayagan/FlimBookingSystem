using Flim.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Interfaces
{
    public interface IFilmService
    {
        Task<int> CreateFilmAsync(FilmDTO filmDto);
        Task<FilmDTO> GetFilmByIdAsync(int id);
    }
}
