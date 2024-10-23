using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Flim.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Flim.Application.ApplicationException;

namespace Flim.Application.Services
{
    public class FilmService : IFilmService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Film> _filmRepository;

        public FilmService(IUnitOfWork unitOfWork, IGenericRepository<Film> filmRepository)
        {
            _unitOfWork = unitOfWork;
            _filmRepository = filmRepository;
        }


        public async Task<int> CreateFilmAsync(AddFilmDTO filmDto)
        {
            var flim = new Film
            {
                Name = filmDto.Name,
                Description = filmDto.Description,
                Duration = filmDto.Duration,
                Genre = filmDto.Genre,

            };

            await _unitOfWork.BeginTransaction();
            await _filmRepository.InsertAsync(flim);
            var result = await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransaction();

            return result;

        }

        public async Task<IEnumerable<Film>> GetFilmAsync()
        {
            var films = await _filmRepository.GetAllAsync(include: film => film
                .Include(f => f.Slots)
                .ThenInclude(s => s.Seats)
                );

            return films;
        }

        public async Task<Film> GetFilmByIdAsync(int id)
        {
            var flim = await _filmRepository.GetByIdAsync(id);

            if (flim is null) throw new NotFoundException("Flim Not Found");
            var slots = await  _unitOfWork.Repository<Slot>().FindAsync(s=>s.FilmId == id);



            flim.Slots = slots.ToList();

            return flim;
        }

        public async Task<IEnumerable<Film>> GetFilmByNameAsync(string name)
        {
            var flim = await _filmRepository.FindAsync(flim => flim.Name == name);

            return flim;


        }
    }
}
