using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Flim.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flim.Domain.Entities;

namespace Flim.Application.Services
{
    public class FilmService : IFilmService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FilmService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<int> CreateFilmAsync(FilmDTO filmDto)
        {
            var flim = new Flim.Domain.Entities.Film
            {
                Name = filmDto.Name,
                Description = filmDto.Description,
                Duration = filmDto.Duration,
                Genre = filmDto.Genre,

            };

            _unitOfWork.BeginTransaction();
            await _unitOfWork.Repository<Film>().InsertAsync(flim);
            _unitOfWork.CommitTransaction();
            var result = await _unitOfWork.SaveAsync();

            return result;

        }

        public async Task<FilmDTO> GetFilmByIdAsync(int id)
        {
            var flim = await _unitOfWork.Repository<Film>().GetByIdAsync(id);

            if (flim is null)
            {
                return null;
            }

            return new FilmDTO
            {
                Name = flim.Name,
                Description = flim.Description,
                Duration = flim.Duration,
                Genre = flim.Genre,
            };

        }

        public async Task<IEnumerable<Film>> GetFilmByNameAsync(string name)
        {
            var flim = await _unitOfWork.Repository<Film>().FindAsync(flim => flim.Name == name);

            return flim;


        }
    }
}
