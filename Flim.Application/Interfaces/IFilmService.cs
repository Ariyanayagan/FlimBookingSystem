﻿using Flim.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flim.Domain.Entities;
using Flim.Application.Records;


namespace Flim.Application.Interfaces
{
    /// <summary>
    /// Contract of film service
    /// </summary>
    public interface IFilmService
    {
        Task<bool> CreateFilmAsync(AddFilmDTO filmDto);
        Task<Film> GetFilmByIdAsync(int id);
        Task<IEnumerable<Film>> GetFilmByNameAsync(string name);
        Task<IEnumerable<Film>> GetFilmByGenreAsync(string name);
        Task<IEnumerable<Film>> GetFilmAsync();

        Task<List<FilmViewModel>> GetSales();
    }
}
